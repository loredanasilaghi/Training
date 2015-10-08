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
        public void TestSumOf13And22UsingBase2()
        {
            Assert.AreEqual(35, CalculateSum(22, 13, 2));
        }

        [TestMethod]
        public void TestSumOf22And23UsingBase2()
        {
            Assert.AreEqual(45, CalculateSum(23, 22, 2));
        }

        [TestMethod]
        public void TestSumOf14And12UsingBase2()
        {
            Assert.AreEqual(26, CalculateSum(12, 14, 2));
        }

        [TestMethod]
        public void TestSumOf43And75UsingBase8()
        {
            Assert.AreEqual(118, CalculateSum(43, 75, 8));
        }
        
        [TestMethod]
        public void TestConversion()
        {
            Assert.AreEqual(20, ConvertToDecimal(ConvertFromDecimal(20, 8), 8));
        }

        [TestMethod]
        public void TestConvertNumberFromDecimal()
        {
            byte[] array = new byte[] { 1, 0, 1, 0 };
            bool areEqual = array.SequenceEqual(ConvertFromDecimal(10, 2));
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void TestConvertNumberToDecimal()
        {
            byte[] array = new byte[] { 1, 0, 1, 0 };
            Assert.AreEqual(10, ConvertToDecimal(array, 2));
        }

        private static int CalculateSum(int firstNumber, int secondNumber, int toBase)
        {
            byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, toBase);
            byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, toBase);
            SwapNumbers(ref firstBinaryNumber, ref secondBinaryNumber);

            int numberKeepInMind = 0;
            int length = 0;
            int difference = 0;
            byte[] sumBinary;
            int sum = 0;
            int mode = 0;
            difference = firstBinaryNumber.Length - secondBinaryNumber.Length;
            length = firstBinaryNumber.Length;
            sumBinary = new byte[length + 1];

            if (difference != 0)
            {
                Array.Resize(ref secondBinaryNumber, length);
                for (int i = secondBinaryNumber.Length - 2; i >= 0; i--)
                {
                    secondBinaryNumber[i + difference] = secondBinaryNumber[i];
                }
                secondBinaryNumber[0] = 0;
            }

            for (int j = length - 1; j >= 0; j--)
            {
                sum = firstBinaryNumber[j] + secondBinaryNumber[j] + numberKeepInMind;
                mode = sum % toBase;
                numberKeepInMind = sum / toBase;
                sumBinary[j + 1] = (byte)mode;
            }

            if (numberKeepInMind == 1)
                sumBinary[0] = 1;
            return ConvertToDecimal(sumBinary, toBase);
        }

        private static void SwapNumbers(ref byte[] firstBinaryNumber, ref byte[] secondBinaryNumber)
        {
            byte[] aux;
            if (firstBinaryNumber.Length < secondBinaryNumber.Length)
            {
                aux = firstBinaryNumber;
                firstBinaryNumber = secondBinaryNumber;
                secondBinaryNumber = aux;
            }
        }

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
    }
}
