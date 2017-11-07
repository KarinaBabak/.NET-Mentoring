﻿using System;
using System.Configuration;

namespace CustomFileSystemWatcher.Config.Elements
{
    public class FolderConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (String)base["path"]; }
            set { base["path"] = value; }
        }
    }
}
