﻿using System;
using System.Configuration;
using CustomFilesWatcher.Config.Collections;
using CustomFilesWatcher.Config.Elements;

namespace CustomFilesWatcher.Config.Sections
{
    public class FileSystemWatcherConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("folders")]
        public FolderElementCollection FoldersCollection
        {
            get { return (FolderElementCollection)base["folders"]; }
        }

        [ConfigurationProperty("rules")]
        public RuleElementCollection RulesCollection
        {
            get { return (RuleElementCollection)base["rules"]; }
        }

        [ConfigurationProperty("culture")]
        public CultureConfigElement CultulreInfo
        {
            get { return ((CultureConfigElement)(base["culture"])); }
        }
    }
}