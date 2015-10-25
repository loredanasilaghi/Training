using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SearchingAndSorting
{
    [TestClass]
    public class Lotto
    {
        [TestMethod]
        public void TestSortedLottoNumbers()
        {
            int[] expected = GenerateLottoExtractions(6);
            QuickSort(expected, 0, 5);
            Assert.IsTrue(IsSorted(expected));
        }

        public static bool IsSorted(int[] numbers)
        {
            bool isSorted = true;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] > numbers[i + 1])
                    isSorted = false;
            }
            return isSorted;
        }

        public static int[] GenerateLottoExtractions(int numberExtractions)
        {
            List<int> entireList = PopulateList(49);
            var random = new Random();
            var result = entireList.OrderBy(item => random.Next()).ToArray();
            int[] resultArray = result.ToArray();
            Array.Resize(ref resultArray, 6);
            return resultArray;
        }

        public static List<int> PopulateList(int numberEntries)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= numberEntries; i++)
            {
                list.Add(i);
            }
            return list;
        }

        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);

                if (pivot > 1)
                    QuickSort(array, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort(array, pivot + 1, right);
            }
        }

        public static int Partition(int[] numbers, int left, int right)
        {
            int pivot = numbers[left];
            while (true)
            {
                while (numbers[left] < pivot)
                    left++;

                while (numbers[right] > pivot)
                    right--;

                if (left < right)
                {
                    Swap(numbers, left, right);
                }
                else
                {
                    return right;
                }
            }
        }

        public static void Swap(int[] numbers, int left, int right)
        {
            int temp = numbers[right];
            numbers[right] = numbers[left];
            numbers[left] = temp;
        }
    }
}
