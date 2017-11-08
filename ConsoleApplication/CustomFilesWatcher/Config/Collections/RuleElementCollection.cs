using System;
using System.Configuration;
using CustomFilesWatcher.Config.Elements;

namespace CustomFilesWatcher.Config.Collections
{
    public class RuleElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleConfigElement)element).Path;
        }
    }
}
