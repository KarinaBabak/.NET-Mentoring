using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public class FileSystemVisitor : IVisitor
    {
        private Func<string, bool> filterPattern;
        private bool stopSearching = false;
        private string[] excludeCriterias = null;

        #region Events

        public delegate void FileSystemHandler(string message);

        /// <summary>
        /// event that appears when search is started
        /// </summary>
        public event FileSystemHandler Start;

        /// <summary>
        /// event that appears when search is finished
        /// </summary>
        public event FileSystemHandler Finish;

        /// <summary>
        /// events that appears when file and folder are found
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FileFound;
        public event FileSystemHandler DirectoryFound;

        public event FileSystemHandler FilteredFileFound;
        public event FileSystemHandler FilteredDirectoryFound;

        #endregion

        #region ctors
        public FileSystemVisitor()
        {
            this.filterPattern = null;
        }
        public FileSystemVisitor(Func<string, bool> filter)
        {
            this.filterPattern = filter;

        }
        #endregion

        protected virtual void OnItemFound(FileSystemEventArgs args)
        {
            var tmp = FileFound;
            if (tmp != null && !args.StopSearching)
            {
                tmp(this, args);
                stopSearching = args.StopSearching;
                excludeCriterias = args.FilesToExclude;
            }
        }

        public IEnumerable<string> Visit(IFileSystemElement element)
        {
            if (string.IsNullOrWhiteSpace(element.Path))
            {
                throw new NullReferenceException("The directory is not exist");
            }

            if (Start != null)
            {
                Start("Start searching");
            }

            foreach (var fileSystemInfo in GetFiles(element.Path, false))
            {
                yield return fileSystemInfo;
            }

            if (Finish != null)
            {
                Finish("Finish searching");
            }
        }

        private IEnumerable<string> GetFiles(string path)
        {
            if (this.stopSearching == true)
            {
                yield break;
            }

            string[] files = System.IO.Directory.GetFiles(path);
            string[] subdirectories = System.IO.Directory.GetDirectories(path);

            foreach (var file in files)
            {
                if (this.excludeCriterias != null && this.excludeCriterias.Contains(file))
                {
                    yield break;
                }

                this.OnItemFound(new FileSystemEventArgs(this.stopSearching));

                if (filterPattern != null && filterPattern.Invoke(file))
                {
                    FilteredFileFound.Invoke("Filtered File found: ");
                }

                yield return file;
            }

            foreach (var subdirectory in subdirectories)
            {
                OnItemFound(new FileSystemEventArgs(stopSearching));
                DirectoryFound.Invoke("Directory found: " + subdirectory);

                if (filterPattern != null && filterPattern.Invoke(subdirectory))
                {
                    FilteredDirectoryFound.Invoke("Filtered Directory found: ");
                }

                foreach (var file in GetFiles(subdirectory))
                {
                    yield return file;
                }
            }
        }
    }
}
