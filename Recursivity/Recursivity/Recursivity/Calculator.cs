using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recursivity
{
    [TestClass]
    public class Calculator
    {
        [TestMethod]
        public void TestInvalidInput()
        {
            Assert.AreEqual(-1, CalculateOperation("* + ee 12 15 20"));
        }

        [TestMethod]
        public void TestCalculationOfHigherIntegerNumbers()
        {
            Assert.AreEqual(540, CalculateOperation("* + 12 15 20"));
        }

        [TestMethod]
        public void TestSimpleCalculation()
        {
            Assert.AreEqual(12.6, CalculateOperation("* 3 4.2"), 0.2);
        }

        [TestMethod]
        public void TestComplexCalculation()
        {
            Assert.AreEqual(1136.25, CalculateOperation("* / * + 56 45 45 3 0.75"));
        }

        [TestMethod]
        public void TestCalculationUsingSubtractAndDivide()
        {
            Assert.AreEqual(-0.5, CalculateOperation("/ - 2 3 2"));
        }

        [TestMethod]
        public void TestCalculationOfSpecialCase()
        {
            Assert.AreEqual(26, CalculateOperation("+ * + 1 2 + 3 4 5"));
        }

        public static double CalculateOperation(string toCalculate)
        {
            char separator = ' ';
            string[] elements = toCalculate.Split(separator);
            int index = FindIndex(elements);
            if (index == -1)
                return -1;

            string operatorVar = elements[index];
            string firstNumberString = elements[index + 1];
            string secondNumberString = elements[index + 2];
            double firstNumber = Convert.ToDouble(firstNumberString);
            double secondNumber = Convert.ToDouble(secondNumberString);
            double result = DoOperation(operatorVar, firstNumber, secondNumber);

            int lastOperatorIndex = toCalculate.LastIndexOf(elements[index]);
            string firstSubstring = toCalculate.Substring(0, lastOperatorIndex);

            int startPosition = lastOperatorIndex + 2 + operatorVar.Length + firstNumberString.Length
                + secondNumberString.Length;// 2 is the number of spaces
            int length = toCalculate.Length - startPosition;
            string secondSubstring = toCalculate.Substring(startPosition, length);

            string substring = firstSubstring + result.ToString() + secondSubstring;
            if (firstSubstring.Length !=0)
            {
                result = CalculateOperation(substring);
            }
            return result;
        }

        public static bool IsOperator(string operatorVar)
        {
            if ((operatorVar == "+") || (operatorVar == "-") || (operatorVar == "*") || (operatorVar == "/"))
                return true;
            else
                return false;
        }

        public static bool IsValidChar(string character)
        {
            if (IsOperator(character))
                return true;
            try
            {
                Convert.ToDouble(character);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
         }

        public static int FindIndex(string[] elements)
        {
            int index = 0;
            for (index = elements.Length - 1; index >= 0; index--)
            {
                if (IsOperator(elements[index]))
                    break;
                if (!IsValidChar(elements[index]))
                    return -1;
            }
            return index;
        }

        public static double DoOperation(string operatorVar, double firstNumber, double secondNumber)
        {
            double result = 0;
            switch (operatorVar)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    result = firstNumber / secondNumber;
                    break;
            }
            return result;
        }
    }
}
