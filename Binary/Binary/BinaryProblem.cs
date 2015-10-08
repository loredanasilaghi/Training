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
            Assert.AreEqual(35, CalculateDecimalSum(22, 13));
        }

        [TestMethod]
        public void TestSumOf22And23()
        {
            Assert.AreEqual(45, CalculateDecimalSum(23, 22));
        }

        [TestMethod]
        public void TestSumOf14And12()
        {
            Assert.AreEqual(26, CalculateDecimalSum(12, 14));
        }

        //[TestMethod]
        //public void TestSubtractOf5And3()
        //{
        //    Assert.AreEqual(2, CalculateSubtract(5, 3));
        //}

        //[TestMethod]
        //public void TestSubtractOf22And13()
        //{
        //    Assert.AreEqual(9, CalculateSubtract(22, 13));
        //}

        private static int CalculateDecimalSum(int firstNumber, int secondNumber)
        {
            //byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, 2);
            //byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, 2);
            return ConvertToDecimal(CalculateBinarySum(firstNumber, secondNumber, 2), 2);
        }

        //private static int CalculateSubtract(int firstNumber, int secondNumber)
        //{
        //    long firstNumberBase2 = CalculateInBase2(firstNumber);
        //    long secondNumberBase2 = CalculateInBase2(secondNumber);
        //    return CalculateInBase10(CalculateBinarySubtract(firstNumberBase2, secondNumberBase2));
        //}

        //private static int CalculateSum(int firstNumber, int secondNumber, int toBase)
        //{
        //    byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, toBase);
        //    byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, toBase);
        //    byte[] aux;
        //    if (firstBinaryNumber.Length < secondBinaryNumber.Length)
        //    {
        //        aux = firstBinaryNumber;
        //        firstBinaryNumber = secondBinaryNumber;
        //        secondBinaryNumber = aux;
        //    }
        //}

        private static byte[] CalculateBinarySum(int firstNumber, int secondNumber, int toBase)
        {
            byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, toBase);
            byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, toBase);
            byte[] aux;
            if (firstBinaryNumber.Length < secondBinaryNumber.Length)
            {
                aux = firstBinaryNumber;
                firstBinaryNumber = secondBinaryNumber;
                secondBinaryNumber = aux;
            }
            int numberKeepInMind = 0;
            int length = 0;
            int difference = 0;
            byte[] sumBinary;
            int sum = 0;
            //byte[] optionalArray;
            difference = firstBinaryNumber.Length - secondBinaryNumber.Length;
            length = firstBinaryNumber.Length;
            //optionalArray = new byte[length];
            sumBinary = new byte[length + 1];
            if (difference != 0)
            {
                Array.Resize(ref secondBinaryNumber, length);
                for (int i = secondBinaryNumber.Length - 1; i >= 0; i--)
                {
                    secondBinaryNumber[i + difference] = secondBinaryNumber[i];
                    //optionalArray[i + difference] = secondBinaryNumber[i];
                }
                secondBinaryNumber[0] = 0;
            }
            for (int j = length - 1; j >= 0; j--)
            {
                //sum = firstBinaryNumber[j] + optionalArray[j] + numberKeepInMind;
                sum = firstBinaryNumber[j] + secondBinaryNumber[j] + numberKeepInMind;
                if (sum == 2)
                {
                    sum = 0;
                    numberKeepInMind = 1;
                }
                else if (sum == 3)
                {
                    sum = 1;
                    numberKeepInMind = 1;
                }
                else
                    numberKeepInMind = 0;
                sumBinary[j + 1] = (byte)sum;
            }

            if (numberKeepInMind == 1)
                sumBinary[0] = 1;
            return sumBinary;
        }

        //private static long CalculateBinarySubtract(long firstNumberBase2, long secondNumberBase2)
        //{
        //    long result = 0;
        //    long number = 0;
        //    int lengthFirstNumber = CalculateLength(firstNumberBase2);
        //    int lengthSecondNumber = CalculateLength(secondNumberBase2);
        //    for (int i = 0; i < lengthFirstNumber; i++)
        //    {
        //        if (i < lengthSecondNumber)
        //        {
        //            result = secondNumberBase2 % 10;
        //            secondNumberBase2 /= 10;
        //            result = result == 0 ? 1 : 0;
        //            number = (long)(number + result * Math.Pow(10, i));
        //        }
        //        else
        //            number= (long)(number + 1 * Math.Pow(10,i));
        //    }
        //    long firstSum = CalculateBinarySum(number, 1);
        //    long finalResult = CalculateBinarySum(firstSum, firstNumberBase2);
        //    int lengthSecondSum = CalculateLength(finalResult);
        //    return finalResult = (long)(finalResult % Math.Pow(10, lengthSecondSum - 1));
        //}

        private static byte[] ConvertFromDecimal(int givenNumber, int toBase)
        {
            List<byte> listArray = new List<byte>();
            while (givenNumber != 0)
            {
                listArray.Add((byte)(givenNumber % toBase));
                givenNumber /= toBase;
            }
            byte[] array = listArray.ToArray();
            Array.Reverse(array);
            return array;
        }

        private static int ConvertToDecimal(byte[] binaryNumber, int fromBase)
        {
            int decimalNumber = 0;
            int power = 0;
            for(int i = binaryNumber.Length - 1; i >=0; i--)
            {
                decimalNumber = (int)(decimalNumber + binaryNumber[i] * Math.Pow(fromBase, power));
                power++;
            }
            return decimalNumber;
        }

        [TestMethod]
        public void TestConvertNumberFromDecimal()
        {
            byte[] array = new byte[] { 1, 0, 1, 0 };

            bool areEqual = array.SequenceEqual(ConvertFromDecimal(10,2)); 
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void TestConvertNumberToDecimal()
        {
            byte[] array = new byte[] { 1, 0, 1, 0 };
            Assert.AreEqual(10, ConvertToDecimal(array, 2));
        }
        
    }
}
