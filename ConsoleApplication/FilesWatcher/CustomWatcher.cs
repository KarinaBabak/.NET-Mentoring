using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Text.RegularExpressions;
using FilesWatcher.Resources;
using System.Threading;
using Models;

namespace FilesWatcher
{
    public class CustomWatcher : ICustomWatcher
    {
        private readonly List<FileSystemWatcher> _fileSystemWatchers = new List<FileSystemWatcher>();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, string> _rules = new Dictionary<string, string>();
        private readonly Dictionary<string, int[]> _rulesOptions = new Dictionary<string, int[]>();
        private readonly List<string> _folders = new List<string>();
        private CultureInfo _currentCulture;
        private readonly string defaultFolderPath = "C:/";

        public CustomWatcher(
            Dictionary<string, string> rules,
            Dictionary<string, int[]> rulesOptions,
            CultureInfo currentCulture,
            List<string> folders)
        {
            _rules = rules;
            _rulesOptions = rulesOptions;
            _currentCulture = currentCulture;
            _folders = folders;

            Thread.CurrentThread.CurrentCulture = _currentCulture;
            Thread.CurrentThread.CurrentUICulture = _currentCulture;

            InitFilesWatchers();
        }

        public CustomWatcher() { }

        private void InitFilesWatchers()
        {
            foreach (var folderPath in _folders)
            {
                _fileSystemWatchers.Add(new FileSystemWatcher(folderPath));
            }
        }

        /// <summary>
        /// Add event handlers and begin watching.
        /// </summary>
        public void SubscribeToChanges()
        {
            foreach (var watcher in _fileSystemWatchers)
            {
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;
                _logger.Info(Messages.BeginWatching);
            }
        }

        /// <summary>
        /// Remove event handlers and stop watching.
        /// </summary>
        public void UnSubscribeOnChanges()
        {
            foreach (var watcher in _fileSystemWatchers)
            {
                watcher.Created -= new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = false;
                _logger.Info(Messages.StopWatching);
            }
        }

        private void OnChanged(object source, FileSystemEventArgs args)
        {
            _logger.Info(Messages.FileChanged);
            var rule = _rules.Where(r => Regex.IsMatch(args.FullPath, r.Key) == true).FirstOrDefault();

            if(rule.Key == null)
            {
                return;
            }

            var currentRuleOptions = _rulesOptions.FirstOrDefault(pair => pair.Key == rule.Value).Value;
            var fileName = TransformFileName(args.Name, rule.Key, currentRuleOptions);
            var newPath = Path.Combine(rule.Value, fileName);

            _logger.Info(Messages.RuleFound);

            if (File.Exists(newPath))
            {
                File.Delete(newPath);
                _logger.Info(Messages.FileRemoved);
            }

            File.Move(args.FullPath, newPath);
            if (File.Exists(newPath))
            {
                _logger.Info(Messages.FileMoved);
            }
        }

        /// <summary>
        /// Transform file name according to rule options
        /// </summary>
        /// <param name="fileName">source file name</param>
        /// <param name="filePath">source file path</param>
        /// <param name="options">rule options</param>
        /// <returns>new file name or not changed file name</returns>
        private string TransformFileName(string fileName, string filePath, int[] options)
        {
            if (options == null)
            {
                return null;
            }

            foreach (var option in options)
            {
                if (option == (int)RuleOptions.IncludeDate)
                {
                    var date = DateTime.Now.ToString("d", _currentCulture);
                    fileName += "_" + date;
                }

                if (option == (int)RuleOptions.IncludeIndex)
                {
                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        var targetPath = Path.Combine(defaultFolderPath, fileName + "_" + i);
                        if (!File.Exists(targetPath))
                        {
                            fileName += "_" + i;
                        }
                        else
                        {
                            fileName += "_0";
                        }
                    }
                }
            }

            return fileName;
        }
    }
}
