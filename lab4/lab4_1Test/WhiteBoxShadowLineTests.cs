using lab4;

namespace lab4_1Test
{
    [TestClass]
    public class WhiteBoxShadowLineTests
    {
        // Путь A: пустой массив
        [TestMethod]
        public void CalculateSum_EmptyArray_ReturnsZero()
        {
            // Arrange
            int[,] coords = new int[0, 2];
            var shadowLine = new ShadowLine(coords);

            // Act
            int result = shadowLine.CalculateSum();

            
            Assert.AreEqual(0, result);
        }

        // Путь BCF: непересекающиеся отрезки
        [TestMethod]
        public void CalculateSum_DisjointSegments_ReturnsSum()
        {
            // Arrange
            int[,] coords = new int[,] { { 1, 3 }, { 5, 7 } };
            var shadowLine = new ShadowLine(coords);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(4, result);
        }

        // Путь BDF: пересечение с расширением вправо
        [TestMethod]
        public void CalculateSum_OverlappingWithExtension_ReturnsSum()
        {
            // Arrange
            int[,] coords = new int[,] { { 1, 3 }, { 2, 5 } };
            var shadowLine = new ShadowLine(coords);
            
            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(4, result);
        }

        // Путь BEF: вложенный отрезок, ничего не делает
        [TestMethod]
        public void CalculateSum_ContainedSegment_ReturnsSum()
        {
            // Arrange
            int[,] coords = new int[,] { { 1, 5 }, { 2, 3 } };
            var shadowLine = new ShadowLine(coords);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(4, result);
        }

        // Путь BDF (граничное расширение): конец одного равен началу другого
        [TestMethod]
        public void CalculateSum_TouchingSegments_ReturnsSum()
        {
            // Arrange
            int[,] coords = new int[,] { { 1, 3 }, { 3, 5 } };

            // Act
            var shadowLine = new ShadowLine(coords);

            // Assert
            int result = shadowLine.CalculateSum();
            Assert.AreEqual(4, result);
        }

    }
}
