using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace Binary
{
    [TestClass]
    public class BinaryProblem
    {
        private enum Operation
        {
            Or = 0,
            And,
            Xor
        }

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

        [TestMethod]
        public void TestOperationOr()
        {
            byte[] first = new byte[] { 1, 0, 0, 1 };
            byte[] second = new byte[] { 1, 1, 0, 1, 0 };
            byte[] expected = new byte[] { 1, 1, 0, 1, 1 };
            Assert.IsTrue(expected.SequenceEqual(CalculateOperation(first, second, Operation.Or)));
        }

        [TestMethod]
        public void TestOperationAnd()
        {
            byte[] first = new byte[] { 1, 0, 0, 1 };
            byte[] second = new byte[] { 1, 1, 0, 1, 0 };
            byte[] expected = new byte[] { 0, 1, 0, 0, 0 };
            Assert.IsTrue(expected.SequenceEqual(CalculateOperation(first, second, Operation.And)));
        }

        [TestMethod]
        public void TestOperationXor()
        {
            byte[] first = new byte[] { 1, 0, 0, 1 };
            byte[] second = new byte[] { 1, 1, 0, 1, 0 };
            byte[] expected = new byte[] { 1, 0, 0, 1, 1 };
            Assert.IsTrue(expected.SequenceEqual(CalculateOperation(first, second, Operation.Xor)));
        }

        [TestMethod]
        public void TestShiftToRight()
        {
            byte[] given = new byte[] { 1, 0, 1, 1 };
            byte[] expected = new byte[] { 0, 0, 1, 0 };
            Assert.IsTrue(expected.SequenceEqual(CalculateShift(given, 2, true)));
        }

        [TestMethod]
        public void TestShiftToLeft()
        {
            byte[] given = new byte[] { 1, 0, 1, 1 };
            byte[] expected = new byte[] { 1, 1, 0, 0 };
            Assert.IsTrue(expected.SequenceEqual(CalculateShift(given, 2, false)));
        }

        private static byte[] CalculateOperation(byte[] firstBinaryNumber, byte[] secondBinaryNumber, Operation operation)
        {
            SwapNumbers(ref firstBinaryNumber, ref secondBinaryNumber);
            AddZerosInArray(ref firstBinaryNumber, ref secondBinaryNumber);

            byte[] result = new byte[firstBinaryNumber.Length];

            for(int i=0; i < result.Length; i++)
            {
                result[i] = Calculate(firstBinaryNumber[i], secondBinaryNumber[i], operation);
            }
            return result;
        }

        private static byte[] CalculateShift(byte[] binaryNumber, int shift, bool toRight)
        {
            if (toRight)
            {
                Array.Reverse(binaryNumber);
            }            
            for (int i = 0; i < shift; i++)
            {
                for (int j = 1; j < binaryNumber.Length; j++)
                {
                    binaryNumber[j - 1] = binaryNumber[j];
                }
                binaryNumber[binaryNumber.Length - 1] = 0;
            }
            if (toRight)
            {
                Array.Reverse(binaryNumber);
            }
            return binaryNumber;
        }

        private static byte Calculate(byte value1, byte value2, Operation operation)
        {
            byte result = 0;
            switch (operation)
            {
                case Operation.Or:
                {
                    if ((value1 == 1) || (value2 == 1))
                        result = 1;
                    else
                        result = 0;
                    break;
                }
                case Operation.And:
                {
                    if ((value1 == 1) && (value2 == 1))
                        result = 1;
                    else
                        result = 0;
                    break;
                }
                case Operation.Xor:
                {
                    if (value1 == value2)
                        result = 0;
                    else
                        result = 1;
                    break;
                }
            }
            return result;
        }

        private static int CalculateSum(int firstNumber, int secondNumber, int toBase)
        {
            byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, toBase);
            byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, toBase);
            SwapNumbers(ref firstBinaryNumber, ref secondBinaryNumber);

            int numberKeepInMind = 0;
            byte[] sumBinary;
            int sum = 0;
            int mode = 0;
            sumBinary = new byte[firstBinaryNumber.Length + 1];
            AddZerosInArray(ref firstBinaryNumber, ref secondBinaryNumber);

            for (int j = firstBinaryNumber.Length - 1; j >= 0; j--)
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

        private static void AddZerosInArray(ref byte[] firstBinaryNumber, ref byte[] secondBinaryNumber)
        {
            int length = 0;
            int difference = 0;
            difference = firstBinaryNumber.Length - secondBinaryNumber.Length;
            length = firstBinaryNumber.Length;

            if (difference != 0)
            {
                Array.Resize(ref secondBinaryNumber, length);
                for (int i = secondBinaryNumber.Length - 2; i >= 0; i--)
                {
                    secondBinaryNumber[i + difference] = secondBinaryNumber[i];
                }
                secondBinaryNumber[0] = 0;
            }
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
