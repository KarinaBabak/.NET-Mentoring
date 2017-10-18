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
        private string[] filePaths = new string[]
        {
            "C:\\Folder\\file1.txt",
            "C:\\Folder\\file2.txt",
            "C:\\Folder\\file1.pptx",
            "C:\\Folder\\file2.mp4",
            "C:\\Folder\\file3.pptx",
            "C:\\Folder\\SubFolder\\file1.txt",
            "C:\\Folder\\SubFolder\\file2.txt"
        };

        private FileSystemVisitor.IFileSystemVisitor _fileSystemVisitor;
        //private Mock<FileSystemVisitor.FileSystemVisitor> _fileSystemVisitor = new Mock<FileSystemVisitor.FileSystemVisitor>();
        private Mock<FileSystemVisitor.Directory> _directoryFileSystemElementMock = new Mock<FileSystemVisitor.Directory>();

        private readonly Mock<EventHandler<FileSystemEventArgs>> fileFoundEventHandlerMock = new Mock<EventHandler<FileSystemEventArgs>>();
        private readonly Mock<FileSystemVisitor.FileSystemVisitor.FileSystemHandler> fileSystemEventHandlerMock = new Mock<FileSystemVisitor.FileSystemVisitor.FileSystemHandler>();
        private readonly Mock<EventHandler> startSearchEventHandlerMock = new Mock<EventHandler>();
        private readonly Mock<EventHandler> finishSearchEventHandlerMock = new Mock<EventHandler>();

        [SetUp]
        public void SetUp()
        {
            _fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
            //_directoryFileSystemElement = new FileSystemVisitor.Directory();

            //_fileSystemVisitor.FileFound += fileFoundEventHandlerMock.Object;
            _fileSystemVisitor.Start += startSearchEventHandlerMock.Object;
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
            //var mockdir = new Mock<FileSystemVisitor.Directory>();
            var directory = new FileSystemVisitor.Directory();

            // act
            _directoryFileSystemElementMock.Setup
                (d => d.Accept(It.IsAny<FileSystemVisitor.IFileSystemVisitor>())).Returns(filePaths);
            startSearchEventHandlerMock.Setup(s => s(It.IsAny<FileSystemVisitor.FileSystemVisitor>(), It.IsAny<EventArgs>())).Verifiable();

            // Assert
            //startSearchEventHandlerMock.Verify();
        }

        [Test]
        public void FileSystemAccessor_FindFiles()
        {
            // arrange
            var directory = new FileSystemVisitor.Directory();
            var _fileSystemVisitor = new Mock<FileSystemVisitor.IFileSystemVisitor>();

            _fileSystemVisitor.Setup(i => i.Visit(directory)).Returns(this.filePaths.ToList());

            // act
            var actualResult = directory.Accept(this._fileSystemVisitor);

            // Assert
            Assert.AreEqual(actualResult.ToList().Count, 7);
        }

    }
}
