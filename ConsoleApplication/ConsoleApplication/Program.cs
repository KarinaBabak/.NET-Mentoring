using FilesWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigFilesWatcher.ConfigSectionReader.LoadConfigSection();
            var rules = ConfigFilesWatcher.ConfigSectionReader.Rules;
            var options = ConfigFilesWatcher.ConfigSectionReader.RulesOptions;
            var culture = ConfigFilesWatcher.ConfigSectionReader.CurrentCultureInfo;
            var foldersPath = ConfigFilesWatcher.ConfigSectionReader.FoldersPath;

            CustomWatcher watcher = new CustomWatcher(
                rules: rules,
                rulesOptions: options,
                currentCulture: culture,
                folders: foldersPath
                );

            watcher.SubscribeToChanges();
            Console.ReadKey();
        }
    }
}
