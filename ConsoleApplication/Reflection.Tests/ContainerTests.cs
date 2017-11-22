using System;
using System.Linq;
using System.IO;
using System.Reflection;
using CustomIoCContainer;
using NUnit.Framework;
using TypesLibrary;

namespace Reflection.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        private readonly string assemblyName = "TypesLibrary.dll";
        private Assembly assembly;
        private Container container;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string[] assemblyLookupPath = new[] {
                AppDomain.CurrentDomain.BaseDirectory,
                Environment.CurrentDirectory,
                Assembly.GetExecutingAssembly().Location
                }.Distinct().ToArray();

            var filePath = assemblyLookupPath
                .Select(f => Path.Combine(f, assemblyName))
                .Where(File.Exists)
                .FirstOrDefault();

            assembly = Assembly.LoadFrom(filePath);
        }

        [SetUp]
        public void SetUp()
        {
            container = new Container();
        }

        #region Test ArgumentNullException

        [Test]
        public void AddAssembly_NullAssembly_ExpectArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                container.AddAssembly(null);
            });
        }

        [Test]
        [TestCase(null, typeof(int))]
        [TestCase(null, null)]
        [TestCase(typeof(int), null)]
        public void AddType_NullType_ExpectArgumentNullException(Type type1, Type type2)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                container.AddType(type1, type2);
            });
            
        }

        [Test]
        public void CreateInstance_NullType_ExpectArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                container.CreateInstance(null);
            });
        }

        #endregion


        #region Test AddAssembly
        [Test]
        public void CreateInstanceOfExportClass()
        {
            container.AddAssembly(assembly);

            var instance = container.CreateInstance(typeof(ExportClass));

            Assert.AreEqual(typeof(ExportClass), instance.GetType());
        }

        [Test]
        public void CreateInstanceOfEnum_ExpectException()
        {
            container.AddAssembly(assembly);

            Assert.Throws<InvalidOperationException>(() =>
            {
                container.CreateInstance(typeof(SuperEnum));
            });
        }

        #endregion

        [Test]
        public void CreateInstanceOfImportClass()
        {
            container.AddType(typeof(SuperClass), typeof(SuperClass));
            container.AddType(typeof(ImportClass), typeof(ImportClass));

            var instance = container.CreateInstance(typeof(ImportClass));

            Assert.AreEqual(typeof(ImportClass), instance.GetType());
            Assert.NotNull(((ImportClass)instance).SuperObject);
        }

        [Test]
        public void AddType_CreateInstanceOfSuperClass()
        {
            container.AddType(typeof(SuperClass), typeof(SuperClass));

            var instance = container.CreateInstance(typeof(SuperClass));

            Assert.AreEqual(typeof(SuperClass), instance.GetType());
        }

        [Test]
        public void AddType_CreateInstanceOfChildClass()
        {
            container.AddType(typeof(ChildSuperClass), typeof(SuperClass));

            var instance = container.CreateInstance(typeof(SuperClass));

            Assert.AreEqual(typeof(ChildSuperClass), instance.GetType());
        }
    }
}
