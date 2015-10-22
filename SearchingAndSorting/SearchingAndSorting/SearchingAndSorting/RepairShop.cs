using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchingAndSorting
{
    [TestClass]
    public class RepairShop
    {
        [TestMethod]
        public void TestRandomCasesStartingWith1()
        {
            int[] initial = { 1, 3, 2, 1, 3, 2 };
            int[] expected = { 1, 1, 2, 2, 3, 3};
            CollectionAssert.AreEqual(expected, QuickSort3(initial, 0, 5));
        }

        [TestMethod]
        public void TestRandomCasesStartingWith2()
        {
            int[] initial = { 2, 3, 2, 1, 3, 3, 2, 1, 1, 2 };
            int[] expected = { 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
            CollectionAssert.AreEqual(expected, QuickSort3(initial, 0, 9));
        }

        [TestMethod]
        public void TestWithoutMediumValue()
        {
            int[] initial = { 3, 1, 3, 1 };
            int[] expected = { 1, 1, 3, 3 };
            CollectionAssert.AreEqual(expected, QuickSort3(initial, 0, 3));
        }

        [TestMethod]
        public void TestWith1Case()
        {
            int[] initial = { 3 };
            int[] expected = { 3 };
            CollectionAssert.AreEqual(expected, QuickSort3(initial, 0, 0));
        }

        [TestMethod]
        public void TestWith2Cases()
        {
            int[] initial = { 3, 2 };
            int[] expected = { 2, 3 };
            CollectionAssert.AreEqual(expected, QuickSort3(initial, 0, 1));
        }

        public static int[] QuickSort3(int[] numbers, int first, int last)
        {
            int pivotValue = 2;
            int lessThan = first;
            int greaterThan = last;
            int element = first;
            while (element <= greaterThan)
            {
                if (numbers[element] < pivotValue)
                {
                    Swap(numbers, lessThan, element);
                    lessThan++;
                    element++;
                }
                if (numbers[element] > pivotValue)
                {
                    Swap(numbers, greaterThan, element);
                    greaterThan--;
                }
                if (numbers[element] == pivotValue)
                    element++;
            }
            return numbers;
        }

        public static void Swap(int[] numbers, int left, int right)
        {
            int temp = numbers[right];
            numbers[right] = numbers[left];
            numbers[left] = temp;
        }
    }
}
