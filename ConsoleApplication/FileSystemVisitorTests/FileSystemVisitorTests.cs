using System;
using System.IO;
using NUnit.Framework;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystemEventArgs = FileSystemVisitor.FileSystemEventArgs;
using System.Collections.Generic;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace FileSystemVisitorTests
{
    [TestFixture]
    public class FileSystemVisitorTests
    {
        private bool isEventHandled = false;
        private FileSystemVisitor.Directory directory; 
        private readonly string testPath = "E:\\Test";
        private string[] filesToExclude = new string[] { "1\\doc1.txt", "1\\doc3.txt" };

        private FileSystemVisitor.IFileSystemVisitor _fileSystemVisitor;

        [SetUp]
        public void SetUp()
        {
            directory = new FileSystemVisitor.Directory(this.testPath);
            //this.isEventHandled = false;
            _fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            //_directoryFileSystemElement = new FileSystemVisitor.Directory();

            //_fileSystemVisitor.FileFound += fileFoundEventHandlerMock.Object;
            //_fileSystemVisitor.Start += startSearchEventHandlerMock.Object;
            //_fileSystemVisitor.Finish += finishSearchEventHandlerMock.Object;
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void FileSystemAccessor_NotExistedDirectory_ThrowsException()
        {
            // arrange
            string invalidPath = "Z:\\abcdefg\\ffffff111111111fff..";
            var directory = new FileSystemVisitor.Directory(invalidPath);

            // act
            directory.Accept(this._fileSystemVisitor);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void FileSystemAccessor_NullDirectory_ThrowsException()
        {
            // arrange
            var directory = new FileSystemVisitor.Directory(null);

            // act
            directory.Accept(this._fileSystemVisitor);
        }

        [Test]
        public void FileSystemAccessor_StartSearch()
        {
            // arrange
            _fileSystemVisitor.Start += HandleTestEvent;

            // act
            var files = this.directory.Accept(this._fileSystemVisitor);
            foreach (var item in files)
            {
                
            }

            // Assert
            Assert.IsTrue(this.isEventHandled);
        }

        [Test]
        public void FileSystemAccessor_FinishSearch()
        {
            //arrange
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.Finish += HandleTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {

            }

            // Assert
            Assert.IsTrue(this.isEventHandled);
        }

        [Test]
        public void FileSystemAccessor_FileFoundEventHandle()
        {
            //arrange
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.FileFound += HandleTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {

            }

            // Assert
            Assert.IsTrue(this.isEventHandled);
        }

        [Test]
        public void FileSystemAccessor_DirectoryFoundEventHandle()
        {
            //arrange
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.DirectoryFound += HandleStringTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {

            }

            // Assert
            Assert.IsTrue(this.isEventHandled);
        }

        [Test]
        public void FileSystemAccessor_FilteredDirectoryFoundEventHandle()
        {
            //arrange
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.FilteredDirectoryFound += HandleStringTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {

            }

            // Assert
            Assert.IsTrue(this.isEventHandled);
        }

        [Test]
        public void FileSystemAccessor_FilteredFileFoundEventHandle()
        {
            //arrange
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.FilteredFileFound += HandleStringTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {

            }

            // Assert
            Assert.IsTrue(this.isEventHandled);
        }

        [Test]
        public void FileSystemAccessor_ReturnFilesFromFolder()
        {
            // arrange
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.FileFound += HandleTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {
                Assert.AreEqual(item.Substring(0, this.testPath.Length), this.testPath);
            }
        }

        [Test]
        public void FileSystemAccessor_StopSearching()
        {
            // arrange
            int counter = 0;
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.FileFound += HandleStopSearchingTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {
                ++counter;
            }

            Assert.AreEqual(counter, 2);
        }

        [Test]
        public void FileSystemAccessor_ExcludeFiles()
        {
            // arrange
            int counter = 0;
            var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            fileSystemVisitor.FileFound += HandleExcludeTestEvent;

            // act
            foreach (var item in this.directory.Accept(fileSystemVisitor))
            {
                foreach (var fileToExclude in this.filesToExclude)
                {
                    if (item.IndexOf(fileToExclude, StringComparison.Ordinal) < 0)
                    {
                        counter++;
                    }
                }
                
            }

            Assert.AreEqual(2, 2);
        }

        private void HandleTestEvent(object sender, EventArgs e)
        {
            this.isEventHandled = true;
        }

        private void HandleStringTestEvent(object sender, string name)
        {
            this.isEventHandled = true;
        }

        private void HandleStopSearchingTestEvent(object sender, FileSystemEventArgs e)
        {
            e.StopSearching = true;
        }
        private void HandleExcludeTestEvent(object sender, FileSystemEventArgs e)
        {
            e.FilesToExclude = this.filesToExclude;
        }
        
    }
}
