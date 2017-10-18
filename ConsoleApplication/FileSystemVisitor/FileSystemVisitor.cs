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
        public event EventHandler<string> DirectoryFound = delegate { };

        public event EventHandler<string> FilteredFileFound = delegate { };
        public event EventHandler<string> FilteredDirectoryFound = delegate { };

        #endregion

        private readonly Func<string, bool> filterPattern;
        private bool stopSearching = false;
        private string[] excludeCriterias = null;
        private string initialPath;

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

            this.initialPath = element.Path;

            OnStart();

            foreach (var fileSystemInfo in GetFiles(this.initialPath))
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
                if (this.IsNeedToBeExcluded(file))
                {
                    yield break;
                }

                this.OnItemFound(new FileSystemEventArgs(this.stopSearching));

                if (filterPattern != null && filterPattern.Invoke(file))
                {
                    OnFilteredFileFound(file);
                }

                yield return file;
            }

            foreach (var subdirectory in subdirectories)
            {
                OnDirectoryFound(subdirectory);

                if (filterPattern != null && filterPattern(subdirectory))
                {
                    OnFilteredDirectoryFound(subdirectory);
                }

                foreach (var file in GetFiles(subdirectory))
                {
                    yield return file;
                }
            }
        }

        private bool IsNeedToBeExcluded(string file)
        {
            int startRelativePathIndex = this.initialPath.Length + 1;
            string relativePathOfCurrentFile = file.Substring(startRelativePathIndex, file.Length - startRelativePathIndex);
            if (this.excludeCriterias != null && this.excludeCriterias.Contains(relativePathOfCurrentFile))
            {
                return true;
            }
            return false;
        }

        private void OnStart()
        {
            Start(this, EventArgs.Empty);
        }

        private void OnFinish()
        {
            Finish(this, EventArgs.Empty);
        }

        private void OnFilteredFileFound(string fileName)
        {
            FilteredFileFound(this, fileName);
        }

        private void OnFilteredDirectoryFound(string directoryName)
        {
            FilteredDirectoryFound(this, directoryName);
        }

        private void OnDirectoryFound(string directoryName)
        {
            DirectoryFound(this, directoryName);
        }
        
    }
}
