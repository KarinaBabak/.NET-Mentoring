﻿using System.Configuration;

namespace ConfigFilesWatcher.Config.Elements
{
    public class CultureConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "ru-Ru", IsKey = true, IsRequired = true)]
        public string Value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }
    }
}