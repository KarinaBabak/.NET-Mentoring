using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NLog;
using System.Text.RegularExpressions;
using FilesWatcher.Resources;
using System.Threading;
using Models;

namespace FilesWatcher
{
    public class CustomWatcher : ICustomWatcher
    {
        private readonly List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, string> rules = new Dictionary<string, string>();
        private readonly Dictionary<string, int[]> rulesOptions = new Dictionary<string, int[]>();
        private readonly List<string> folders = new List<string>();
        private CultureInfo currentCulture;
        private readonly string defaultFolderPath = "C:/";

        public CustomWatcher(
            Dictionary<string, string> rules,
            Dictionary<string, int[]> rulesOptions,
            CultureInfo currentCulture,
            List<string> folders)
        {
            this.rules = rules;
            this.rulesOptions = rulesOptions;
            this.currentCulture = currentCulture;
            this.folders = folders;

            Thread.CurrentThread.CurrentCulture = this.currentCulture;
            Thread.CurrentThread.CurrentUICulture = this.currentCulture;

            InitFilesWatchers();
        }

        public CustomWatcher() { }

        private void InitFilesWatchers()
        {
            foreach (var folderPath in folders)
            {
                fileSystemWatchers.Add(new FileSystemWatcher(folderPath));
            }
        }

        /// <summary>
        /// Add event handlers and begin watching.
        /// </summary>
        public void SubscribeToChanges()
        {
            foreach (var watcher in fileSystemWatchers)
            {
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;
                logger.Info(Messages.BeginWatching);
            }
        }

        /// <summary>
        /// Remove event handlers and stop watching.
        /// </summary>
        public void UnSubscribe()
        {
            foreach (var watcher in fileSystemWatchers)
            {
                watcher.Created -= new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = false;
                logger.Info(Messages.StopWatching);
            }
        }

        private void OnChanged(object source, FileSystemEventArgs args)
        {
            logger.Info(Messages.FileChanged);
            var rule = rules.Where(r => Regex.IsMatch(args.FullPath, r.Key) == true).FirstOrDefault();

            if(rule.Key == null)
            {
                return;
            }

            var currentRuleOptions = rulesOptions.FirstOrDefault(pair => pair.Key == rule.Value).Value;
            var fileName = TransformFileName(args.Name, rule.Key, currentRuleOptions);
            var newPath = Path.Combine(rule.Value, fileName);

            logger.Info(Messages.RuleFound);

            if (File.Exists(newPath))
            {
                File.Delete(newPath);
                logger.Info(Messages.FileRemoved);
            }

            File.Move(args.FullPath, newPath);
            if (File.Exists(newPath))
            {
                logger.Info(Messages.FileMoved);
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
                    var date = DateTime.Now.ToString("d", currentCulture);
                    fileName += "_" + date;
                }

                if (option == (int)RuleOptions.IncludeIndex)
                {
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        fileName = Path.Combine(defaultFolderPath, fileName + "_" + i);
                    }
                }
            }

            return fileName;
        }
    }
}
