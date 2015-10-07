using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace Binary
{
    [TestClass]
    public class BinaryProblem
    {
        [TestMethod]
        public void TestSumOf13And22()
        {
            Assert.AreEqual(35, CalculateSum(13, 22));
        }

        [TestMethod]
        public void TestSumOf22And23()
        {
            Assert.AreEqual(45, CalculateSum(23, 22));
        }

        [TestMethod]
        public void TestSubtractOf5And3()
        {
            Assert.AreEqual(2, CalculateSubtract(5, 3));
        }

        [TestMethod]
        public void TestSubtractOf22And13()
        {
            Assert.AreEqual(9, CalculateSubtract(22, 13));
        }

        private static int CalculateSum(int firstNumber, int secondNumber)
        {
            long firstNumberBase2 = CalculateInBase2(firstNumber);
            long secondNumberBase2 = CalculateInBase2(secondNumber);
            return CalculateInBase10(CalculateBinarySum(firstNumberBase2, secondNumberBase2));
        }

        private static int CalculateSubtract(int firstNumber, int secondNumber)
        {
            long firstNumberBase2 = CalculateInBase2(firstNumber);
            long secondNumberBase2 = CalculateInBase2(secondNumber);
            return CalculateInBase10(CalculateBinarySubtract(firstNumberBase2, secondNumberBase2));
        }

        private static long CalculateBinarySum(long firstNumberBase2, long secondNumberBase2)
        { 
            int numberKeepInMind = 0;
            long result1 = 0;
            long result2 = 0;
            long result = 0;
            long sum = 0;
            int power = 0;
            while ((firstNumberBase2 != 0) || (secondNumberBase2 != 0) || (numberKeepInMind!=0))
            {
                result1 = (long)firstNumberBase2 % 10;
                firstNumberBase2 /= 10;
                result2 = (long)secondNumberBase2 % 10;
                secondNumberBase2 /= 10;
                result = result1 + result2 + numberKeepInMind;
                if (result == 2)
                {
                    result = 0;
                    numberKeepInMind = 1;
                }
                else if (result == 3)
                {
                    result = 1;
                    numberKeepInMind = 1;
                }
                else
                    numberKeepInMind = 0;
                sum = (long)(sum + result * Math.Pow(10, power));
                power++;
            }
            return sum; 
        }

        private static long CalculateBinarySubtract(long firstNumberBase2, long secondNumberBase2)
        {
            long result = 0;
            long number = 0;
            int lengthFirstNumber = CalculateLength(firstNumberBase2);
            int lengthSecondNumber = CalculateLength(secondNumberBase2);
            for (int i = 0; i < lengthFirstNumber; i++)
            {
                if (i < lengthSecondNumber)
                {
                    result = secondNumberBase2 % 10;
                    secondNumberBase2 /= 10;
                    result = result == 0 ? 1 : 0;
                    number = (long)(number + result * Math.Pow(10, i));
                }
                else
                    number= (long)(number + 1 * Math.Pow(10,i));
            }
            long firstSum = CalculateBinarySum(number, 1);
            long finalResult = CalculateBinarySum(firstSum, firstNumberBase2);
            int lengthSecondSum = CalculateLength(finalResult);
            return finalResult = (long)(finalResult % Math.Pow(10, lengthSecondSum - 1));
        }

        private static long CalculateInBase2(int number)
        {
            long binaryNumber = 0;
            int power = 0;
            int result = 0;
            while (number != 0)
            {
                result = number % 2;
                number /= 2;
                binaryNumber = (long)(binaryNumber + result * Math.Pow(10, power));
                power++;
            }
            return binaryNumber;
        }

        private static int CalculateInBase10(long number)
        {
            int intNumber = 0;
            int power = 0;
            long result = 0;
            while (number != 0)
            {
                result = number % 10;
                number /= 10;
                intNumber = (int)(intNumber + result * Math.Pow(2, power));
                power++;
            }
            return intNumber;
        }

        private static byte[] ConvertNumber(int givenNumber, int givenBase)
        {
            List<byte> listArray = new List<byte>();
            while (givenNumber != 0)
            {
                listArray.Add((byte)(givenNumber % givenBase));
                givenNumber /= givenBase;
            }
            byte[] array = listArray.ToArray();
            Array.Reverse(array);
            return array;
        }

        [TestMethod]
        public void TestConvertNumber()
        {
            byte[] array = new byte[] { 1, 0, 1, 0};

            bool areEqual = array.SequenceEqual(ConvertNumber(10,2)); // true
            Assert.IsTrue(areEqual);
        }
        



        private static int CalculateLength(long number)
        {
            int count = 0;
            while (number != 0)
            {
                number /= 10;
                count++;
            }
            return count;
        }
    }
}
