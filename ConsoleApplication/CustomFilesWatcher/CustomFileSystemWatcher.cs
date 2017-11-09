using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using CustomFilesWatcher.Config.Elements;
using CustomFilesWatcher.Config.Sections;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using System.Threading;
using CustomFilesWatcher.Resources;
using System.Text.RegularExpressions;
//using CustomFilesWatcher.Resources;

namespace CustomFilesWatcher
{
    public class CustomFileSystemWatcher
    {
        private readonly List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
        private readonly string defaultFolderPath = "D:/test";
        private readonly FileSystemWatcherConfigSection configSection;
        private readonly Logger _logger = LogManager.GetLogger(typeof(CustomFileSystemWatcher).FullName);
        private readonly Dictionary<string, string> rules = new Dictionary<string, string>();
        private readonly Dictionary<string, int[]> rulesOptions = new Dictionary<string, int[]>();

        private readonly string dateTimeFormat;

        public CustomFileSystemWatcher()
        {
            // Get the section from configuration file.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configSection = ConfigurationManager.GetSection("fileSystemWatcher") as FileSystemWatcherConfigSection;
            _logger = LogManager.GetLogger(typeof(CustomFileSystemWatcher).Name);

            if (configSection == null)
            {
                throw new ArgumentNullException(nameof(configSection));
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(configSection.CultulreInfo.Value);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(configSection.CultulreInfo.Value);

            // add folders to listen changes
            foreach (var folder in configSection.FoldersCollection)
            {
                fileSystemWatchers.Add(new FileSystemWatcher(((FolderConfigElement)folder).Path));
            }

            // add values to rules dictionary
            foreach (var rule in configSection.RulesCollection)
            {
                RuleConfigElement ruleElement = (RuleConfigElement)rule;
                rules.Add(ruleElement.Filter, ruleElement.Path);
                rulesOptions.Add(ruleElement.Path, new int[2] { (int)ruleElement.IncludeDate, (int)ruleElement.IncludeIndex });
            }

            SubscribeToChanges();
        }

        /// <summary>
        /// Add event handlers and begin watching.
        /// </summary>
        public void SubscribeToChanges()
        {
            foreach (var watcher in fileSystemWatchers)
            {
                //watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;
                _logger.Info(Messages.BeginWatching);
            }
        }

        private void OnChanged(object source, FileSystemEventArgs args)
        {
            _logger.Info(Messages.FileChanged);
            var rule = rules.Where(r => Regex.IsMatch(args.FullPath, r.Key) == true).FirstOrDefault();

            var currentRuleOptions = rulesOptions.FirstOrDefault(pair => pair.Key == rule.Value).Value;
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

            foreach (var option in options)
            {
                if (option == (int)RuleOptions.IncludeDate)
                {
                    var date = dateTimeFormat == null ? string.Empty : DateTime.Now.ToString(dateTimeFormat);
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

