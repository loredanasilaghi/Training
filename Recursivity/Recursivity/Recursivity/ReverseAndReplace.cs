using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recursivity
{
    [TestClass]
    public class ReverseAndReplace
    {
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
        public void TestReplaceCharFromString()
        {
            Assert.AreEqual("massassappa", ReplaceChar("mississippi", 'i', 'a'));
        }

        public static string ReverseGivenString(string givenString)
        {

            if (givenString.Length > 0)
            {
                string substring = givenString.Substring(0, givenString.Length - 1);
                string reversedSubstring = ReverseGivenString(substring);
                return givenString[givenString.Length - 1] + reversedSubstring;
            }
            else
                return givenString;
        }

        public static string ReplaceChar(string word, char toBeReplaced, char replace)
        {
            if (word.Length > 0)
            {
                if (word[0] == toBeReplaced)
                    word = word.Replace(word[0], replace);
                string substring = word.Substring(1, word.Length - 1);
                string replacedSubstring = ReplaceChar(substring, toBeReplaced, replace);
                return word[0] + replacedSubstring;
            }
            else
                return word;
        }
    }
}
