using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using CustomIoCContainer;
using NUnit.Framework;

namespace Reflection.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        private Assembly assembly = Assembly.LoadFrom("TypesLibrary");
        Container container;

        [SetUp]
        public void SetUp()
        {
            container = new Container();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAssembly_NullAssembly_ExpectArgumentNullException()
        {
            container.AddAssembly(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddType_NullType_ExpectArgumentNullException()
        {
            container.AddType(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTypeWithTwoParameters_NullTypes_ExpectArgumentNullException()
        {
            container.AddType(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTypeWithTwoParameters_OneNullType_ExpectArgumentNullException()
        {
            container.AddType(null, typeof(int));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstance_NullType_ExpectArgumentNullException()
        {
            container.CreateInstance(null);
        }

    }
}
