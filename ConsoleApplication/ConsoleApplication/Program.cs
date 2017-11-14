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
            ConfigFilesWatcher.CustomFileSystemWatcher.Initialize();
            var rules = ConfigFilesWatcher.CustomFileSystemWatcher.Rules;
            var options = ConfigFilesWatcher.CustomFileSystemWatcher.RulesOptions;
            var culture = ConfigFilesWatcher.CustomFileSystemWatcher.CurrentCultureInfo;
            var foldersPath = ConfigFilesWatcher.CustomFileSystemWatcher.FoldersPath;

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
