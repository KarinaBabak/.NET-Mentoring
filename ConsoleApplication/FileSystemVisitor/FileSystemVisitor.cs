using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        #region Events

        public delegate void FileSystemHandler(string message);

        /// <summary>
        /// event that appears when search is started
        /// </summary>
        public event EventHandler Start = delegate { };

        /// <summary>
        /// event that appears when search is finished
        /// </summary>
        public event EventHandler Finish = delegate { };

        /// <summary>
        /// events that appears when file and folder are found
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FileFound = delegate { };
        public event FileSystemHandler DirectoryFound = delegate { };

        public event FileSystemHandler FilteredFileFound = delegate { };
        public event FileSystemHandler FilteredDirectoryFound = delegate { };

        #endregion

        private readonly Func<string, bool> filterPattern;
        private bool stopSearching = false;
        private string[] excludeCriterias = null;

        #region ctors
        public FileSystemVisitor(Func<string, bool> filter = null)
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
                throw new NullReferenceException("The path is not correct");
            }

            if (!System.IO.Directory.Exists(element.Path))
            {
                throw new DirectoryNotFoundException("The directory is not exist");
            }

            OnStart();

            foreach (var fileSystemInfo in GetFiles(element.Path))
            {
                yield return fileSystemInfo;
            }

            OnFinish();
        }

        private IEnumerable<string> GetFiles(string path)
        {
            if (this.stopSearching)
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
                    FilteredFileFound("Filtered File found: ");
                }

                yield return file;
            }

            foreach (var subdirectory in subdirectories)
            {
                OnItemFound(new FileSystemEventArgs(stopSearching));
                DirectoryFound("Directory found: " + subdirectory);

                if (filterPattern != null && filterPattern(subdirectory))
                {
                    FilteredDirectoryFound("Filtered Directory found: " + subdirectory);
                }

                foreach (var file in GetFiles(subdirectory))
                {
                    yield return file;
                }
            }
        }

        private void OnStart()
        {
            Start(this, EventArgs.Empty);
        }

        private void OnFinish()
        {
            Finish(this, EventArgs.Empty);
        }
    }
}
