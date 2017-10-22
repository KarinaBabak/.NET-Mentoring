using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringParser;

namespace _2.ParserTests
{
    [TestClass]
    public class StringParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseToInt_NullString_ShouldThrowException()
        {
            StringParser.StringParser.ParseToInt(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseToInt_EmptyString_ShouldThrowException()
        {
            StringParser.StringParser.ParseToInt("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseToInt_StringWithWhitespaces_ShouldThrowException()
        {
            StringParser.StringParser.ParseToInt("    ");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseToInt_StringOfLetters_ShouldThrowException()
        {
            StringParser.StringParser.ParseToInt("ppp");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseToInt_StringWithLetters_ShouldThrowException()
        {
            StringParser.StringParser.ParseToInt("1p");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseToInt_StringWithSpecialSymbol_ShouldThrowException()
        {
            StringParser.StringParser.ParseToInt("22$33");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ParseToInt_HugeString_ShouldThrowOverflowException()
        {
            StringParser.StringParser.ParseToInt(int.MaxValue.ToString() + "666");
        }

        [TestMethod]
        public void ParseToInt_StringWithDigits_ShouldThrowException()
        {
            int actualResult = StringParser.StringParser.ParseToInt("123456");

            Assert.AreEqual(123456, actualResult);
        }

        [TestMethod]
        public void ParseToInt_StringWithMinus_ShouldThrowException()
        {
            int actualResult = StringParser.StringParser.ParseToInt("-23456");

            Assert.AreEqual(-23456, actualResult);
        }

        [TestMethod]
        public void ParseToInt_StringWithPlus_ShouldThrowException()
        {
            int actualResult = StringParser.StringParser.ParseToInt("+23456");

            Assert.AreEqual(23456, actualResult);
        }

        [TestMethod]
        public void ParseToInt_StringWithFirstWhitespace_ShouldThrowException()
        {
            int actualResult = StringParser.StringParser.ParseToInt(" 23456");

            Assert.AreEqual(23456, actualResult);
        }
    }
}
