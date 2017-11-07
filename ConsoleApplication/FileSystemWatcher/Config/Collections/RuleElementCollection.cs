using System;
using System.Configuration;
using CustomFileSystemWatcher.Config.Elements;

namespace CustomFileSystemWatcher.Config.Collections
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
