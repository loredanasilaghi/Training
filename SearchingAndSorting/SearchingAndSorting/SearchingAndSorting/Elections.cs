using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchingAndSorting
{
    [TestClass]
    public class Elections
    {
        [TestMethod]
        public void TestWith2Sections()
        {
            CandidateResult[] section1Results = new CandidateResult[] { new CandidateResult("Candidate1", 20), new CandidateResult("Candidate3", 15),
                new CandidateResult("Candidate2", 14), new CandidateResult("Candidate4", 12) };
            CandidateResult[] section2Results = new CandidateResult[] { new CandidateResult("Candidate2", 35), new CandidateResult("Candidate3", 19),
                new CandidateResult("Candidate1", 17), new CandidateResult("Candidate4", 3) };
            CandidateResult[][] results = new CandidateResult[][]
            {
                section1Results,
                section2Results
            };
            CandidateResult[] expectedFinalResults = new CandidateResult[] { new CandidateResult("Candidate2", 49), new CandidateResult("Candidate1", 37),
                new CandidateResult("Candidate3", 34), new CandidateResult("Candidate4", 15) };
            CandidateResult[] actualResults = OrderElectionsResultList(results);
            CollectionAssert.AreEqual(expectedFinalResults, actualResults);
        }

        [TestMethod]
        public void TestWith3Sections()
        {
            CandidateResult[] section1Results = new CandidateResult[] { new CandidateResult("Candidate1", 20), new CandidateResult("Candidate3", 15),
                new CandidateResult("Candidate2", 14), new CandidateResult("Candidate4", 12) };
            CandidateResult[] section2Results = new CandidateResult[] { new CandidateResult("Candidate4", 39), new CandidateResult("Candidate1", 22),
                new CandidateResult("Candidate3", 17), new CandidateResult("Candidate2", 16) };
            CandidateResult[] section3Results = new CandidateResult[] { new CandidateResult("Candidate2", 35), new CandidateResult("Candidate3", 19),
                new CandidateResult("Candidate1", 17), new CandidateResult("Candidate4", 3) };

            CandidateResult[][] results = new CandidateResult[][]
            {
                section1Results,
                section2Results,
                section3Results
            };
            CandidateResult[] expectedFinalResults = new CandidateResult[] { new CandidateResult("Candidate2", 65), new CandidateResult("Candidate1", 59),
                new CandidateResult("Candidate4", 54), new CandidateResult("Candidate3", 51) };
            CandidateResult[] actualResults = OrderElectionsResultList(results);
            CollectionAssert.AreEqual(expectedFinalResults, actualResults);
        }

        public struct CandidateResult
        {
            public string candidateName;
            public int votes;
            public CandidateResult(string candidateName, int votes)
            {
                this.candidateName = candidateName;
                this.votes = votes;
            }
        }

        public static CandidateResult[] OrderElectionsResultList(CandidateResult[][] results)
        {
            CandidateResult[] finalResults = CreateElectionResultList(results);
            
            OrderList(finalResults);
            return finalResults;
        }

        private static void OrderList(CandidateResult[] finalResults)
        {
            for (int i = 1; i <= finalResults.Length - 1; i++)
            {
                CandidateResult candidate = finalResults[i];
                int position = i - 1;
                while ((position >= 0) && (finalResults[position].votes < candidate.votes))
                {
                    Swap(finalResults, position +1 , position);
                    position--;
                }
                finalResults[position + 1] = candidate;
            }
        }

        public static CandidateResult[] CreateElectionResultList(CandidateResult[][] results)
        {
            CandidateResult[] cumulatedResults = new CandidateResult[results[0].Length];
            cumulatedResults = ResultsRecievesFirstSectionResults(results);
            for (int i = 1; i <= results.Length - 1; i++)
            {
                CandidateResult[] currentSection = results[i];
                for (int j = 0; j <= currentSection.Length - 1; j++)
                {
                    for (int h = 0; h <= cumulatedResults.Length - 1; h++)
                    {
                        if (String.Compare(cumulatedResults[h].candidateName, currentSection[j].candidateName) == 0)
                        {
                            cumulatedResults[h].votes += currentSection[j].votes;
                            break;
                        }
                    }
                }
            }
            return cumulatedResults;
        }

        private static CandidateResult[] ResultsRecievesFirstSectionResults(CandidateResult[][] results)
        {
            CandidateResult[] firstsectionResults = new CandidateResult[results[0].Length];
            for (int i = 0; i <= results[0].Length - 1; i++)
            {
                firstsectionResults[i] = results[0][i];
            }
            return firstsectionResults;
        }

        public static void Swap(CandidateResult[] results, int left, int right)
        {
            CandidateResult temp = results[right];
            results[right] = results[left];
            results[left] = temp;
        }
    }
}
