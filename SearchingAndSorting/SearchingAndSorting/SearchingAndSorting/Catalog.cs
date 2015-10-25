using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchingAndSorting
{
    [TestClass]
    public class Catalog
    {
        [TestMethod]
        public void TestSortCatalogByGeneralScore()
        {
            Student[] catalog = InitializeCatalog();
            Student[] expectedCatalog = InitializeCatalog(true);
            Student[] actualCatalog = SortStudentsBasedOnGeneralScore(catalog);
            Assert.IsTrue(CompareCatalogs(expectedCatalog, actualCatalog));
        }

        [TestMethod]
        public void TestFindStudendWithSpecificGeneralScore()
        {
            Student[] catalog = InitializeCatalog();
            double generalScore = 7.8;
            string expectedStudent = "Student3";
            Student actualStudent = FindStudentByGeneralScore(catalog, generalScore);
            Assert.AreEqual(expectedStudent, actualStudent.name);
        }

        [TestMethod]
        public void TestFindStudentWithLowestGeneralScore()
        {
            Student[] catalog = InitializeCatalog();
            string expectedStudent = "Student4";
            Student actualStudent = FindStudentWithLowestGeneralScore(catalog);
            Assert.AreEqual(expectedStudent, actualStudent.name);
        }

        [TestMethod]
        public void TestFindStudentWithMostOf10s()
        {
            Student[] catalog = InitializeCatalog();
            string expectedStudent = "Student2";
            Student actualStudent = FindStudentWithMostOf10s(catalog);
            Assert.AreEqual(expectedStudent, actualStudent.name);
        }

        public static bool CompareCatalogs(Student[] catalog1, Student[] catalog2)
        {
            bool areEqual = true;
            for (int i = 0; i < catalog1.Length; i++)
            {
                if (catalog1[i].name != catalog2[i].name)
                    areEqual = false;
            }
            return areEqual;
        }

        private static Student[] InitializeCatalog(bool actual = false)
        {
            Subject[] student1Subjects = new Subject[]
            {
                new Subject(SubjectsNames.Biology, new int[] { 10, 8}),
                new Subject(SubjectsNames.English, new int[] { 7, 6}),
                new Subject(SubjectsNames.Geography, new int[] { 9, 10}),
                new Subject(SubjectsNames.Informatics, new int[] { 5, 6}),
                new Subject(SubjectsNames.Math, new int[] { 7, 5}),
            };

            Subject[] student2Subjects = new Subject[]
            {
                new Subject(SubjectsNames.Biology, new int[] { 10, 9}),
                new Subject(SubjectsNames.English, new int[] { 8, 9}),
                new Subject(SubjectsNames.Geography, new int[] { 7, 10}),
                new Subject(SubjectsNames.Informatics, new int[] { 10, 9}),
                new Subject(SubjectsNames.Math, new int[] { 10, 8}),
            };

            Subject[] student3Subjects = new Subject[]
            {
                new Subject(SubjectsNames.Biology, new int[] { 10, 8}),
                new Subject(SubjectsNames.English, new int[] { 10, 10}),
                new Subject(SubjectsNames.Geography, new int[] { 8, 8}),
                new Subject(SubjectsNames.Informatics, new int[] { 7, 6}),
                new Subject(SubjectsNames.Math, new int[] { 6, 5}),
            };

            Subject[] student4Subjects = new Subject[]
            {
                new Subject(SubjectsNames.Biology, new int[] { 5, 6}),
                new Subject(SubjectsNames.English, new int[] { 7, 6}),
                new Subject(SubjectsNames.Geography, new int[] { 8, 8}),
                new Subject(SubjectsNames.Informatics, new int[] { 5, 7}),
                new Subject(SubjectsNames.Math, new int[] { 7, 5}),
            };

            Subject[] student5Subjects = new Subject[]
            {
                new Subject(SubjectsNames.Biology, new int[] { 10, 7}),
                new Subject(SubjectsNames.English, new int[] { 7, 10}),
                new Subject(SubjectsNames.Geography, new int[] { 9, 10}),
                new Subject(SubjectsNames.Informatics, new int[] { 8, 8}),
                new Subject(SubjectsNames.Math, new int[] { 5, 9}),
            };

            if (actual)
            {
                Student[] students = new Student[]
                {
                new Student("Student2", student2Subjects, 9),
                new Student("Student5", student5Subjects, 8.3),
                new Student("Student3", student3Subjects, 7.8),
                new Student("Student1", student1Subjects, 7.3),
                new Student("Student4", student4Subjects, 6.4)
                };
                return students;
            }
            else
            {
                Student[] students = new Student[]
                {
                new Student("Student1", student1Subjects),
                new Student("Student2", student2Subjects),
                new Student("Student3", student3Subjects),
                new Student("Student4", student4Subjects),
                new Student("Student5", student5Subjects)
                };
                return students;
            }
        }

        public struct Student
        {
            public string name;
            public Subject[] subjects;
            public double generalScore;
            public int numberOf10;
            public Student(string name, Subject[] subjects, double generalScore=0, int numberOf10 =0)
            {
                this.name = name;
                this.subjects= subjects;
                this.generalScore = generalScore;
                this.numberOf10 = numberOf10;
            }
        }

        public struct Subject
        {
            public SubjectsNames subjectName;
            public int[] scores;

            public Subject(SubjectsNames subjectName, int[] scores)
            {
                this.subjectName = subjectName;
                this.scores = scores;
            }
        }

        public enum SubjectsNames
        {
            English = 0,
            Math,
            Informatics,
            Geography,
            Biology
        }

        public static Student FindStudentWithMostOf10s(Student[] catalog)
        {
            int numberOf10s = CalculateNumberOf10s(ref catalog);
            for (int i = 0; i <= catalog.Length - 1; i++)
            {
                if (catalog[i].numberOf10 == numberOf10s)
                    return catalog[i];
            }
            return new Student();
        }

        public static int CalculateNumberOf10s(ref Student[] catalog)
        {
            int maxNumberOf10s = 0;
            for (int i = 0; i <= catalog.Length - 1; i++)
            {
                int numberOf10s = 0;
                for (int j = 0; j <= catalog[i].subjects.Length - 1; j++)
                {
                    for (int h = 0; h <= catalog[i].subjects[j].scores.Length - 1; h++)
                    {
                        if (catalog[i].subjects[j].scores[h] == 10)
                            numberOf10s++;
                    }
                }
                catalog[i].numberOf10 = numberOf10s;
                if (numberOf10s > maxNumberOf10s)
                    maxNumberOf10s = numberOf10s;
            }
            return maxNumberOf10s;
        }

        public static Student FindStudentByGeneralScore(Student[] student, double generalScore)
        {
            Student[] sortedCatalog = SortStudentsBasedOnGeneralScore(student);
            for (int i = 0; i <= sortedCatalog.Length - 1; i++)
            {
                if (generalScore == sortedCatalog[i].generalScore)
                    return sortedCatalog[i];
            }
            return new Student();
        }

        public static Student FindStudentWithLowestGeneralScore(Student[] student)
        {
            Student[] sortedCatalog = SortStudentsBasedOnGeneralScore(student);
            return sortedCatalog[sortedCatalog.Length - 1];
        }
        public static void CalculateGeneralScore(ref Student student)
        {
            double sum = 0;
            for (int i = 0; i < student.subjects.Length; i++)
            {
                sum += CalculateAverageSubjectScore(student.subjects[i]);
            }
            student.generalScore = sum / student.subjects.Length;
        }

        public static double CalculateAverageSubjectScore(Subject subject)
        {
            double sum = 0;
            for (int i = 0; i < subject.scores.Length; i++)
            {
                sum += subject.scores[i];
            }
            return sum / subject.scores.Length;
        }

        public static Student[] SortStudentsBasedOnGeneralScore(Student[] catalog)
        {
            for (int i = 0; i < catalog.Length; i++)
            {
                CalculateGeneralScore(ref catalog[i]);
            }
            QuickSort(ref catalog, 0, catalog.Length-1);
            Array.Reverse(catalog);
            return catalog;
        }

        public static void QuickSort(ref Student[] student, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(ref student, left, right);

                if (pivot > 1)
                    QuickSort(ref student, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort(ref student, pivot + 1, right);
            }
        }

        public static int Partition(ref Student[] student, int left, int right)
        {
            double pivot = student[left].generalScore;
            while (true)
            {
                while (student[left].generalScore < pivot)
                    left++;

                while (student[right].generalScore > pivot)
                    right--;

                if (left < right)
                {
                    Swap(ref student, left, right);
                }
                else
                {
                    return right;
                }
            }
        }

        public static void Swap(ref Student[] student, int left, int right)
        {
            Student temp = student[right];
            student[right] = student[left];
            student[left] = temp;
        }
    }
}
