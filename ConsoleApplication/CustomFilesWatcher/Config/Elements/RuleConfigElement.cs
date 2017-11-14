using System;
using System.Configuration;

namespace ConfigFilesWatcher.Config.Elements
{
    public class RuleConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = false, IsRequired = true, DefaultValue = "C:/default_folder")]
        public string Path
        {
            get { return (String)base["path"]; }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("filter", IsKey = true, IsRequired = true)]
        public string Filter
        {
            get { return (String)base["filter"]; }
            set { base["filter"] = value; }
        }

        [ConfigurationProperty("includeDate", DefaultValue = RuleOptions.IncludeDate, IsKey = false, IsRequired = false)]
        public RuleOptions IncludeDate
        {
            get { return (RuleOptions)base["includeDate"]; }
            set { base["includeDate"] = value; }
        }

        [ConfigurationProperty("includeIndex", DefaultValue = RuleOptions.None, IsKey = false, IsRequired = false)]
        public RuleOptions IncludeIndex
        {
            get { return (RuleOptions)base["includeIndex"]; }
            set { base["includeIndex"] = value; }
        }
    }
}
