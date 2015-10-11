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
            Assert.AreEqual(35, CalculateSumOfIntegerNumbers(22, 13, 2));
        }

        [TestMethod]
        public void TestSumOf22And23UsingBase2()
        {
            Assert.AreEqual(45, CalculateSumOfIntegerNumbers(23, 22, 2));
        }

        [TestMethod]
        public void TestSumOf14And12UsingBase2()
        {
            Assert.AreEqual(26, CalculateSumOfIntegerNumbers(12, 14, 2));
        }

        [TestMethod]
        public void TestSumOf43And75UsingBase8()
        {
            Assert.AreEqual(118, CalculateSumOfIntegerNumbers(43, 75, 8));
        }
        
        [TestMethod]
        public void TestSubtractUsingBase2()
        {
            Assert.AreEqual(2, CalculateSubtractOfIntegerNumbers(5, 3, 2));
        }

        [TestMethod]
        public void TestSubtractOf22And13UsingBase2()
        {
            Assert.AreEqual(9, CalculateSubtractOfIntegerNumbers(22, 13, 2));
        }

        [TestMethod]
        public void TestSubtractOf43And75UsingBase8()
        {
            Assert.AreEqual(32, CalculateSubtractOfIntegerNumbers(43, 75, 8));
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
            byte[] first =new byte[] { 1, 0, 0, 1 };
            byte[] second = new byte[] { 1, 1, 0, 1, 0 };
            byte[] expected = new byte[] { 1, 1, 0, 1, 1 };
            CollectionAssert.AreEqual(expected, CalculateOperation(first, second, Operation.Or));
        }

        [TestMethod]
        public void TestOperationAnd()
        {
            byte[] first = new byte[] { 1, 0, 0, 1 };
            byte[] second = new byte[] { 1, 1, 0, 1, 0 };
            byte[] expected = new byte[] { 0, 1, 0, 0, 0 };
            CollectionAssert.AreEqual(expected,(CalculateOperation(first, second, Operation.And)));
        }

        [TestMethod]
        public void TestOperationXor()
        {
            byte[] first = new byte[] { 1, 0, 0, 1 };
            byte[] second = new byte[] { 1, 1, 0, 1, 0 };
            byte[] expected = new byte[] { 1, 0, 0, 1, 1 };
            CollectionAssert.AreEqual(expected,(CalculateOperation(first, second, Operation.Xor)));
        }

        [TestMethod]
        public void TestShiftToRight()
        {
            byte[] given = new byte[] { 1, 0, 1, 1 };
            byte[] expected = new byte[] { 0, 0, 1, 0 };
            CollectionAssert.AreEqual(expected,(CalculateRightShift(given, 2)));
        }

        [TestMethod]
        public void TestShiftToLeft()
        {
            byte[] given = new byte[] { 1, 0, 1, 1 };
            byte[] expected = new byte[] { 1, 0, 1, 1, 0, 0, 0};
            CollectionAssert.AreEqual(expected, (CalculateLeftShift(given, 3)));
        }

        [TestMethod]
        public void TestNot()
        {
            byte[] given = new byte[] { 1, 0 };
            byte[] expected = new byte[] { 0, 1 };
            CollectionAssert.AreEqual(expected, (ComputeNotByteArray(given)));
        }

        private static byte[] CalculateOperation(byte[] firstBinaryNumber, byte[] secondBinaryNumber, Operation operation)
        {
            SwapNumbers(ref firstBinaryNumber, ref secondBinaryNumber);
            secondBinaryNumber = AddZerosInArray(firstBinaryNumber, secondBinaryNumber);
            return DoOperation(firstBinaryNumber, secondBinaryNumber, operation);
        }

        private static byte[] DoOperation(byte[] firstBinaryNumber, byte[] secondBinaryNumber, Operation operation)
        {
            byte[] result = new byte[firstBinaryNumber.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Calculate(firstBinaryNumber[i], secondBinaryNumber[i], operation);
            }

            return result;
        }

        private static byte[] CalculateLeftShift(byte[] binaryNumber, int shift)
        {
            for (int i = 1; i  <= shift; i++)
            {
                Array.Resize(ref binaryNumber, binaryNumber.Length + 1);
            }
            return binaryNumber;
        }

        private static byte[] CalculateRightShift(byte[] binaryNumber, int shift)
        {
            for (int i = 0; i < shift;i++)
            {
                for (int j = binaryNumber.Length - 2; j>=0; j--)
                {
                    binaryNumber[j + 1] = binaryNumber[j];
                }
                binaryNumber[0] = 0;
            }
            return binaryNumber;
        }

        private static byte Calculate(byte value1, byte value2, Operation operation)
        {
            switch (operation)
            {
                case Operation.Or:
                        return ComputeOr(value1, value2);
                case Operation.And:
                        return ComputeAnd(value1, value2);
                case Operation.Xor:
                        return ComputeXor(value1, value2);
            }
            return 0;
        }

        private static byte ComputeXor(byte value1, byte value2)
        {
            return (byte)((value1 == value2) ? 0 : 1);
        }

        private static byte ComputeAnd(byte value1, byte value2)
        {
            return (byte) (((value1 == 1) && (value2 == 1)) ? 1 : 0);
        }

        private static byte ComputeOr(byte value1, byte value2)
        {
            return (byte)(((value1 == 1) || (value2 == 1)) ? 1 : 0);
        }

        private static byte ComputeNotByte(byte value)
        {
            return (byte)((value==1) ? 0 : 1);
        }

        private static byte[] ComputeNotByteArray(byte[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                value[i] = ComputeNotByte(value[i]);
            }
            return value;
        }

        private static byte[] MoveToLeft(byte[] binaryNumber)
        {
            for (int j = 1; j < binaryNumber.Length; j++)
            {
                binaryNumber[j - 1] = binaryNumber[j];
            }
            Array.Resize(ref binaryNumber, binaryNumber.Length - 1);
            return binaryNumber;
        }

        private static int CalculateSumOfIntegerNumbers(int firstNumber, int secondNumber, int toBase)
        {
            byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, toBase);
            byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, toBase);
            return ConvertToDecimal(CalculateSum(firstBinaryNumber, secondBinaryNumber, toBase), toBase);
        }

        private static int CalculateSubtractOfIntegerNumbers(int firstNumber, int secondNumber, int toBase)
        {
            if(firstNumber<secondNumber)
            {
                int aux = firstNumber;
                firstNumber = secondNumber;
                secondNumber = aux;
            }
            byte[] firstBinaryNumber = ConvertFromDecimal(firstNumber, toBase);
            byte[] secondBinaryNumber = ConvertFromDecimal(secondNumber, toBase);
            return ConvertToDecimal(CalculateSubtract(firstBinaryNumber, secondBinaryNumber, toBase), toBase);
        }

        private static byte[] CalculateSum(byte[] firstBinaryNumber, byte[] secondBinaryNumber, int toBase)
        {
            SwapNumbers(ref firstBinaryNumber, ref secondBinaryNumber);
            byte[] sumBinary = new byte[firstBinaryNumber.Length + 1];
            secondBinaryNumber = AddZerosInArray(firstBinaryNumber, secondBinaryNumber);
            int numberKeepInMind = 0;
            for (int j = firstBinaryNumber.Length - 1; j >= 0; j--)
            {
                int sum = firstBinaryNumber[j] + secondBinaryNumber[j] + numberKeepInMind;
                int mode = sum % toBase;
                numberKeepInMind = sum / toBase;
                sumBinary[j + 1] = (byte)mode;
            }

            if (numberKeepInMind == 1)
                sumBinary[0] = 1;
            else
                sumBinary = MoveToLeft(sumBinary);
            return sumBinary;
        }

        private static byte[] CalculateSubtract(byte[] firstBinaryNumber, byte[] secondBinaryNumber, int toBase)
        {
            secondBinaryNumber = AddZerosInArray(firstBinaryNumber, secondBinaryNumber);
            int noKeepInMind = 0;
            byte[] subtractBinary = new byte[secondBinaryNumber.Length];
            for (int i = secondBinaryNumber.Length - 1; i >= 0; i--)
            {
                if (noKeepInMind == 1)
                {
                    firstBinaryNumber[i] = (byte) (firstBinaryNumber[i] - 1);
                }
                if (secondBinaryNumber[i] > firstBinaryNumber[i])
                {
                    noKeepInMind = 1;
                   firstBinaryNumber[i] = (byte)(firstBinaryNumber[i] + toBase);
                }
                else
                    noKeepInMind = 0;

                subtractBinary[i] = (byte)(firstBinaryNumber[i] - secondBinaryNumber[i]);
            }

            return subtractBinary;
        }

        private static byte[] AddZerosInArray(byte[] firstBinaryNumber, byte[] secondBinaryNumber)
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
            return secondBinaryNumber;
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
