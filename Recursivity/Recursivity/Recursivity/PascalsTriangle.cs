using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Recursivity
{
    [TestClass]
    public class PascalsTriangle
    {
        [TestMethod]
        public void TestWithRow4()
        {
            int rowNumber = 4;
            int[][] expected = new int[rowNumber][];
            expected[0] = new int[] { 1 };
            expected[1] = new int[] { 1, 1 };
            expected[2] = new int[] { 1, 2, 1 };
            expected[3] = new int[] { 1, 3, 3, 1 };
            for (int i = 0; i < rowNumber; i++)
                CollectionAssert.AreEqual(expected[i], GenerateRow(rowNumber)[i]);
        }

        [TestMethod]
        public void TestWithRow0()
        {
            int[][] expected = { };
            CollectionAssert.AreEqual(expected, GenerateRow(0));
        }

        [TestMethod]
        public void TestWithRow1()
        {
            int rowNumber = 1;
            int[][] expected = new int[rowNumber] [];
            expected[0] = new int[] { 1 };
            for (int i = 0; i<rowNumber; i++)
                CollectionAssert.AreEqual(expected[i], GenerateRow(rowNumber)[i]);
        }

        [TestMethod]
        public void TestWithRow2()
        {
            int rowNumber = 2;
            int[][] expected = new int[2][];
            expected[0] = new int[] { 1 };
            expected[1] = new int[] { 1, 1 };
            for (int i = 0; i < rowNumber; i++)
                CollectionAssert.AreEqual(expected[i], GenerateRow(rowNumber)[i]);
        }

        public static int[][] GenerateRow(int rowNumber)
        {
            int[][] triangle = new int[rowNumber][];
            for (int i = 1; i <= rowNumber; i++)
            {
                triangle[i-1] = new int[i];
                for (int j = 0; j < i; j++)
                {
                    triangle[i-1][j] = GenerateNumbers(i, j);
                }
            }
            return triangle;
        }
        
        public static int GenerateNumbers(int row, int column)
        {
            if (row == 0 || column == 0 || row == column + 1)
                return 1;
            int leftNumber = 0;
            int rightNumber = 0;
            leftNumber = GenerateNumbers(row - 1, column - 1);
            rightNumber = GenerateNumbers(row - 1, column);
            return leftNumber + rightNumber;
        }

    }
}
