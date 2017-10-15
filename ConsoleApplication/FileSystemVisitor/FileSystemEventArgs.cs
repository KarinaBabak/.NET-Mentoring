using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public class FileSystemEventArgs : EventArgs
    {
        public bool StopSearching { get; set; }

        public string[] FilesToExclude { get; set; }

        public FileSystemEventArgs()
        {
            this.StopSearching = false;
        }

        public FileSystemEventArgs(bool stopSearching)
        {
            this.StopSearching = stopSearching;
            this.FilesToExclude = null;
        }

        public FileSystemEventArgs(bool stopSearching, string[] filesToExclude)
        {
            this.StopSearching = stopSearching;
            this.FilesToExclude = filesToExclude;
        }
    }
}
