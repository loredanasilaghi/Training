using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recursivity
{
    [TestClass]
    public class ReverseAndReplace
    {
        [TestMethod]
        public void TestReverseEmptyString()
        {
            Assert.AreEqual("", ReverseGivenString(""));
        }
        

        [TestMethod]
        public void TestReverseString()
        {
            Assert.AreEqual("jihgfedcba", ReverseGivenString("abcdefghij"));
        }

        [TestMethod]
        public void TestReverseWord()
        {
            Assert.AreEqual("drink", ReverseGivenString("knird"));
        }

        [TestMethod]
        public void TestReplaceCharFromShortString()
        {
            Assert.AreEqual("txa", ReplaceChar("tea", 'e', 'x'));
        }

        [TestMethod]
        public void TestReplaceCharFromEmptyString()
        {
            Assert.AreEqual("", ReplaceChar("", 'e', 'x'));
        }

        [TestMethod]
        public void TestReplaceCharNotFromString()
        {
            Assert.AreEqual("tea", ReplaceChar("tea", 'i', 'x'));
        }

        [TestMethod]
        public void TestReplaceCharFromString()
        {
            Assert.AreEqual("massassappa", ReplaceChar("mississippi", 'i', 'a'));
        }

        [TestMethod]
        public void TestReplaceCharStartingWithFromString()
        {
            Assert.AreEqual("anataally", ReplaceChar("initially", 'i', 'a'));
        }

        public static string ReverseGivenString(string givenString)
        {

            if (givenString.Length == 0) return givenString;
            string substring = givenString.Substring(0, givenString.Length - 1);
            string reversedSubstring = ReverseGivenString(substring);
            return givenString[givenString.Length - 1] + reversedSubstring;
        }

        public static string ReplaceChar(string word, char toBeReplaced, char replacement)
        {
            if (word.Length == 0) return word;
            char firstChar = word[0];
            char replacedfirstChar = ReplaceFirstChar(firstChar, toBeReplaced, replacement);
            string substring = word.Substring(1, word.Length - 1);
            string replacedSubstring = ReplaceChar(substring, toBeReplaced, replacement);
            return replacedfirstChar + replacedSubstring;
        }

        public static char ReplaceFirstChar(char firstChar, char toBeReplaced, char replacement)
        {
            if (firstChar == toBeReplaced)
                firstChar = replacement;
            return firstChar;
        }

    }
}
