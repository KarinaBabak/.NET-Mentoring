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
        [TestCase(null)]
        [TestCase(null, null)]
        [TestCase(typeof(int), null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddType_NullType_ExpectArgumentNullException(Type type1, Type type2)
        {
            container.AddType(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstance_NullType_ExpectArgumentNullException()
        {
            container.CreateInstance(null);
        }

    }
}
