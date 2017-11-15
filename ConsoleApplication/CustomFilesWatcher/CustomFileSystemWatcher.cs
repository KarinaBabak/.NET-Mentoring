using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using ConfigFilesWatcher.Config.Elements;
using ConfigFilesWatcher.Config.Sections;
using System.Globalization;


namespace ConfigFilesWatcher
{
    public static class CustomFileSystemWatcher
    {
        private static readonly List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
        private static readonly Dictionary<string, string> rules = new Dictionary<string, string>();
        private static readonly Dictionary<string, int[]> rulesOptions = new Dictionary<string, int[]>();
        private static CultureInfo currentCulture;
        private static List<string> folders =  new List<string>();

        public static Dictionary<string, string> Rules
        {
            get { return rules; }
        }

        public static Dictionary<string, int[]> RulesOptions
        {
            get { return rulesOptions; }
        }

        public static CultureInfo CurrentCultureInfo
        {
            get { return currentCulture; }
        }

        public static List<FileSystemWatcher> FilesWatchers
        {
            get { return fileSystemWatchers; }
        }

        public static List<string> FoldersPath
        {
            get { return folders; }
        }

        public static void Initialize()
        {
            // Get the section from configuration file.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            FileSystemWatcherConfigSection configSection = 
                ConfigurationManager.GetSection("fileSystemWatcher") as FileSystemWatcherConfigSection;

            if (configSection == null)
            {
                throw new ArgumentNullException(String.Format("Config section {0} does not exist", nameof(configSection)));
            }

            currentCulture = new CultureInfo(configSection.CultulreInfo.Value);

            // add folders to listen changes
            foreach (var folder in configSection.FoldersCollection)
            {
                folders.Add(((FolderConfigElement)folder).Path);
            }

            // add values to rules dictionary
            foreach (var rule in configSection.RulesCollection)
            {
                RuleConfigElement ruleElement = (RuleConfigElement)rule;
                rules.Add(ruleElement.Filter, ruleElement.Path);
                rulesOptions.Add(ruleElement.Path, new int[2] { (int)ruleElement.IncludeDate, (int)ruleElement.IncludeIndex });
            }
        }
    }
}

