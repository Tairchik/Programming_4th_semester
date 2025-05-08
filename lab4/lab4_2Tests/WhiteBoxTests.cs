using lab4_2;

namespace lab4_2Tests
{
    [TestClass]
    public sealed class WhiteBoxTests
    {
        [TestMethod]
        public void StatementCoverage()
        {
            var expected = "c  c - c/a*";
            var actual = Poliz.ConvertToPolishNotation("(( c - c )/ c)*a");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BranchCoverage()
        {
            var expected = "acb/+ff+-";
            var actual = Poliz.ConvertToPolishNotation("a+c/b-(f+f)");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConditionCoverage()
        {
            var expected = "a  f  с  с-*-";
            var actual = Poliz.ConvertToPolishNotation("a - f * (с - с)");
            Assert.AreEqual(expected, actual);

            actual = Poliz.ConvertToPolishNotation("a - f * (с - с))");

            actual = Poliz.ConvertToPolishNotation("a - f * ((с - с)");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MultipleConditionCoverage()
        {
            var expected = "a  b+";
            var actual = Poliz.ConvertToPolishNotation("a + b");
            Assert.AreEqual(expected, actual);

            expected = "a  f  с  с-*-";
            actual = Poliz.ConvertToPolishNotation("a - f * (с - с)");
            Assert.AreEqual(expected, actual);

            actual = Poliz.ConvertToPolishNotation("a - f * (с - с))");

            actual = Poliz.ConvertToPolishNotation("a - f * ((с - с)");
        }

    }
}
