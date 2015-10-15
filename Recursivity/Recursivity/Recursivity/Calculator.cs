using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recursivity
{
    [TestClass]
    public class Calculator
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(540, CalculateOperation("* + 12 15 20"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(12.6, CalculateOperation("* 3 4.2"));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(1136.25, CalculateOperation("* / * + 56 45 45 3 0.75"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(-0.5, CalculateOperation("/ - 2 3 2"));
        }

        public static double CalculateOperation(string toCalculate)
        {
            char separator = ' ';
            string[] elements = toCalculate.Split(separator);
            string operatorVar = elements[0];
            string lastElement = elements[elements.Length - 1].Replace('.', ',');
            double lastNumber = Convert.ToDouble(lastElement);
            if (lastElement == operatorVar)
                return lastNumber;
            int length = (int)(toCalculate.Length - 2 - operatorVar.Length - lastElement.Length);
            string substring = toCalculate.Substring(operatorVar.Length + 1, length);

            
            switch (operatorVar)
            {
                case  "+":
                    return Math.Round(CalculateOperation(substring) + lastNumber, 2);
                case "-":
                    return Math.Round(CalculateOperation(substring) - lastNumber, 2);
                case "*":
                    return Math.Round(CalculateOperation(substring) * lastNumber, 2);
                case "/":
                    return Math.Round(CalculateOperation(substring) / lastNumber, 2);
                default:
                    return lastNumber;
            }
            
        }
    }
}
