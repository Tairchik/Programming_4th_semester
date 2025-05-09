﻿using lab4_2;

namespace lab4_2Tests
{
    [TestClass]
    public sealed class PostfixNotationTests
    {

        // Тесты на корректные выражения

        [TestMethod]
        public void ConvertToPolishNotation_NumericInput_ReturnsCorrectNotation()
        {
            string input = "1 + 2 * 3";
            string expected = "1  2  3*+";
            string result = Poliz.ConvertToPolishNotation(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToPolishNotation_PriorityOperators_ReturnsCorrectNotation()
        {
            string input = "a + b * c";
            string expected = "a  b  c*+";
            string result = Poliz.ConvertToPolishNotation(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToPolishNotation_ParenthesesOverridePriority_ReturnsCorrectNotation()
        {
            string input = "(a + b) * c";
            string expected = "a  b+  c*";
            string result = Poliz.ConvertToPolishNotation(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToPolishNotation_ComplexExpression_ReturnsCorrectNotation()
        {
            string input = "a + b * (c - d) / e";
            string expected = "a  b  c  d- * e/+";
            string result = Poliz.ConvertToPolishNotation(input);
            Assert.AreEqual(expected, result);
        }

        // Тесты на граничные случаи
        [TestMethod]
        public void ConvertToPolishNotation_EmptyInput_ReturnsEmptyString()
        {
            string input = "";
            string expected = "";
            string result = Poliz.ConvertToPolishNotation(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToPolishNotation_SingleVariable_ReturnsSameVariable()
        {
            string input = "x";
            string expected = "x";
            string result = Poliz.ConvertToPolishNotation(input);
            Assert.AreEqual(expected, result);
        }


        // Тесты на некорректные выражения
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConvertToPolishNotation_UnbalancedParentheses_ThrowsOrExits()
        {
            string input = "(a + b";
            Poliz.ConvertToPolishNotation(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertToPolishNotation_InvalidCharacters_IgnoresOrFails()
        {
            string input = "a + b # c";
            string result = Poliz.ConvertToPolishNotation(input);
        }

    }
}
