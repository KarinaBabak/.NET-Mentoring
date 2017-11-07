using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using CustomFileSystemWatcher.Config.Elements;
using CustomFileSystemWatcher.Config.Collections;
using CustomFileSystemWatcher.Config.Sections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace CustomFileSystemWatcher
{
    public class CustomFileSystemWatcher
    {
        private readonly List<FileSystemWatcher> FileSystemWatchers = new List<FileSystemWatcher>();

    }
}
