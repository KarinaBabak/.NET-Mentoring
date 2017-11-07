using System;
using System.Configuration;
using CustomFileSystemWatcher.Config.Elements;

namespace CustomFileSystemWatcher.Config.Collections
{
    public class FolderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderConfigElement)element).Path;

        }

        [ConfigurationCollection(typeof(FolderConfigElement),
            AddItemName = "folder")]
        [ConfigurationProperty("folders")]
        public FolderElementCollection Folders
        {
            get { return (FolderElementCollection)base["folders"]; }
        }
    }
}
