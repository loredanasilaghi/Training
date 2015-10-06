using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binary
{
    [TestClass]
    public class BinaryProblem
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(35, CalculateSum(13, 22));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(45, CalculateSum(23, 22));
        }

        private static int CalculateSum(int firstNumber, int secondNumber)
        {
            long firstBase2 = CalculateInBase2(firstNumber);
            long secondBase2 = CalculateInBase2(secondNumber);
            return CalculateInBase10(CalculateBinarySum(firstBase2, secondBase2));
        }

        private static long CalculateBinarySum(long firstBase2, long secondBase2)
        { 
            int numberKeepInMind = 0;
            long result1 = 0;
            long result2 = 0;
            long result = 0;
            long sum = 0;
            int power = 0;
            while ((firstBase2 != 0) || (secondBase2 != 0) || (numberKeepInMind!=0))
            {
                result1 = (long)firstBase2 % 10;
                firstBase2 /= 10;
                result2 = (long)secondBase2 % 10;
                secondBase2 /= 10;
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
    }
}
