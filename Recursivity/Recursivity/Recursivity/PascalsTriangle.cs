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
                CollectionAssert.AreEqual(expected[i], GenerateTriangle(rowNumber)[i]);
        }
        
        [TestMethod]
        public void TestWithRow1()
        {
            int rowNumber = 1;
            int[][] expected = new int[rowNumber] [];
            expected[0] = new int[] { 1 };
            for (int i = 0; i<rowNumber; i++)
                CollectionAssert.AreEqual(expected[i], GenerateTriangle(rowNumber)[i]);
        }

        [TestMethod]
        public void TestWithRow2()
        {
            int rowNumber = 2;
            int[][] expected = new int[2][];
            expected[0] = new int[] { 1 };
            expected[1] = new int[] { 1, 1 };
            for (int i = 0; i < rowNumber; i++)
                CollectionAssert.AreEqual(expected[i], GenerateTriangle(rowNumber)[i]);
        }

        public static int[][] GenerateTriangle(int rowNumber)
        {
            int[][] triangle = new int[rowNumber][];
            rowNumber--;
            InitializeTriangle(rowNumber, ref triangle);
            PopulateTriangle(rowNumber, ref triangle);
            GenerateNumbers(ref triangle, 0, 0);
            return triangle;
        }

        private static void InitializeTriangle(int rowNumber, ref int[][] triangle)
        {
            for (int row = 0; row <= rowNumber; row++)
            {
                triangle[row] = new int[row + 1];
            }
        }

        private static void PopulateTriangle(int rowNumber, ref int[][] triangle)
        {
            for (int column = 0; column <= rowNumber; column++)
            {
                GenerateNumbers(ref triangle, rowNumber, column);
            }
        }
        
        public static int GenerateNumbers(ref int[][] triangle, int row, int column)
        {
            if(triangle[row][column]!=0)
                return triangle[row][column];
            if (row == 0 || column == 0 || row == column)
            {
                triangle[row][column] = 1;
                return 1;
            }
            int leftNumber = GenerateNumbers(ref triangle, row - 1, column - 1);
            int rightNumber = GenerateNumbers(ref triangle, row - 1, column);
            int value = leftNumber + rightNumber;
            triangle[row][column] = value;
            return value;
        }

    }
}
