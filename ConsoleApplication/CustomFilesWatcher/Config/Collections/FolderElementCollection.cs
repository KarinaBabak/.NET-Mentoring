using System;
using System.Configuration;
using ConfigFilesWatcher.Config.Elements;

namespace ConfigFilesWatcher.Config.Collections
{
    [ConfigurationCollection(typeof(FolderConfigElement))]
    public class FolderElementCollection : ConfigurationElementCollection
    {
        protected override string ElementName => "folder";
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderConfigElement)element).Path;

        }
    }
}
