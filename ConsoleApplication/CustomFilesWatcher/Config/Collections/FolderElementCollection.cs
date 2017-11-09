using System;
using System.Configuration;
using CustomFilesWatcher.Config.Elements;

namespace CustomFilesWatcher.Config.Collections
{
    [ConfigurationCollection(typeof(FolderConfigElement))]
    public class FolderElementCollection : ConfigurationElementCollection
    {
        protected override string ElementName => "folder";
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderConfigElement)element).Path;

        }

        //[ConfigurationCollection(typeof(FolderConfigElement),
        //    AddItemName = "folder")]
        //[ConfigurationProperty("folders")]
        //public FolderElementCollection Folders
        //{
        //    get { return (FolderElementCollection)base["folders"]; }
        //}
    }
}
