using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Recursivity
{
    [TestClass]
    public class HanoiTowers
    {
        [TestMethod]
        public void TestWith3Disks()
        {
            int diskNumber = 3;
            List<int> towerA = new List<int> (PopulateList(diskNumber));
            List<int> towerB = new List<int>();
            List<int> towerAux = new List<int>();
            List<int> expected = new List<int>(towerA);
            MoveDisks(diskNumber, ref towerA, ref towerAux, ref towerB);
            CollectionAssert.AreEqual(expected, towerB);
        }

        [TestMethod]
        public void TestWith1Disk()
        {
            int diskNumber = 1;
            List<int> towerA = new List<int>(PopulateList(diskNumber));
            List<int> towerB = new List<int>();
            List<int> towerAux = new List<int>();
            List<int> expected = new List<int>(towerA);
            MoveDisks(diskNumber, ref towerA, ref towerAux, ref towerB);
            CollectionAssert.AreEqual(expected, towerB);
        }

        [TestMethod]
        public void TestWith24Disks()
        {
            int diskNumber = 24;
            List<int> towerA = new List<int>(PopulateList(diskNumber));
            List<int> towerB = new List<int>();
            List<int> towerAux = new List<int>();
            List<int> expected = new List<int>(towerA);
            MoveDisks(diskNumber, ref towerA, ref towerAux, ref towerB);
            CollectionAssert.AreEqual(expected, towerB);
        }

        public static void MoveDisks(int diskNumber, ref List<int> initial, ref List<int> auxiliar, ref List<int> final)
        {
            int position = initial.IndexOf(diskNumber);

            if (position == 0)
            {
                initial.Remove(diskNumber);
                final.Insert(0, diskNumber);
            }

            else
            {
                int nextDisk = initial[position - 1];
                MoveDisks(nextDisk, ref initial, ref final, ref auxiliar);
                MoveDisks(diskNumber, ref initial, ref auxiliar, ref final);
                MoveDisks(nextDisk, ref auxiliar, ref initial, ref final);
            }
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
    }
}
