using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringHelper;

namespace _1.StringHelperTest
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(EmptyArgumentException))]
        public void SetNullString_ShouldThrowEmptyArgumentException()
        {
            var stringHelper = new StringHelper.StringHelper();
            stringHelper.InputValue = null;
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyArgumentException))]
        public void SetEmptyString_ShouldThrowEmptyArgumentException()
        {
            var stringHelper = new StringHelper.StringHelper();
            stringHelper.InputValue = "";
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyArgumentException))]
        public void SetStringWithWhiteSpaces_ShouldThrowEmptyArgumentException()
        {
            var stringHelper = new StringHelper.StringHelper();
            stringHelper.InputValue = "             ";
        }

        [TestMethod]
        public void GetFirstStringSymbol_ShouldReturnFirstSympolOfString()
        {
            var stringHelper = new StringHelper.StringHelper();
            stringHelper.InputValue = "string";
            var actualResult = stringHelper.GetFirstStringSymbol();

            Assert.AreEqual('s', actualResult);
        }

        [TestMethod]
        public void GetFirstStringSymbol_ShouldReturnFirstSympoOfTrimmedString()
        {
            var stringHelper = new StringHelper.StringHelper();
            stringHelper.InputValue = " string";
            var actualResult = stringHelper.GetFirstStringSymbol();

            Assert.AreEqual('s', actualResult);
        }
    }
}
