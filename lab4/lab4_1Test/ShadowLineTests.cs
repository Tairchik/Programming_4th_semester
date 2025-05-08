using lab4;

namespace lab4_1Test
{
    [TestClass]
    public class ShadowLineTests
    {
        [TestMethod]
        public void CalculateSum_SingleSegment_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 5 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void CalculateSum_MultipleNonOverlappingSegments_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 3 }, { 5, 7 }, { 9, 12 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(7, result); // (3-1) + (7-5) + (12-9)
        }

        [TestMethod]
        public void CalculateSum_PartiallyOverlappingSegments_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 5 }, { 3, 7 }, { 6, 10 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(9, result); // (5-1) + (7-5) + (10-7)
        }

        [TestMethod]
        public void CalculateSum_CompletelyOverlappingSegments_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 10 }, { 2, 5 }, { 6, 8 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(9, result); // (10-1)
        }

        [TestMethod]
        public void CalculateSum_SegmentsWithCommonPoints_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 3 }, { 3, 5 }, { 5, 7 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(6, result); // (3-1) + (5-3) + (7-5)
        }

        [TestMethod]
        public void CalculateSum_EmptyArray_ReturnsZero()
        {
            // Arrange
            int[,] coordinates = new int[0, 2];
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateSum_PointSegments_ReturnsZero()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 1 }, { 3, 3 }, { 5, 5 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(0, result); // (1-1) + (3-3) + (5-5)
        }

        [TestMethod]
        public void CalculateSum_UnsortedSegments_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 9, 12 }, { 1, 3 }, { 5, 7 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(7, result); // (3-1) + (7-5) + (12-9)
        }

        [TestMethod]
        public void Constructor_ValidInput_CreatesObject()
        {
            // Arrange & Act
            int[,] coordinates = new int[,] { { 1, 5 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Assert
            Assert.IsNotNull(shadowLine);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidColumnCount_ThrowsArgumentException()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 1, 5, 7 } };

            // Act & Assert
            ShadowLine shadowLine = new ShadowLine(coordinates); // Должен выдать исключение ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_LeftBoundaryGreaterThanRightBoundary_ThrowsArgumentException()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 5, 1 } };

            // Act & Assert
            ShadowLine shadowLine = new ShadowLine(coordinates); // Должен выдать исключение ArgumentException
        }

        [TestMethod]
        public void Constructor_SortsSegments()
        {
            // Arrange
            int[,] coordinates = new int[,] { { 9, 12 }, { 1, 3 }, { 5, 7 } };

            // Act
            ShadowLine shadowLine = new ShadowLine(coordinates);
            int[,] result = shadowLine.Coordinates;

            // Assert
            Assert.AreEqual(1, result[0, 0]);
            Assert.AreEqual(3, result[0, 1]);
            Assert.AreEqual(5, result[1, 0]);
            Assert.AreEqual(7, result[1, 1]);
            Assert.AreEqual(9, result[2, 0]);
            Assert.AreEqual(12, result[2, 1]);
        }

        [TestMethod]
        public void CalculateSum_NegativeValues_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { -5, -2 }, { 1, 4 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            Assert.AreEqual(6, result); // (-2-(-5)) + (4-1)
        }

        [TestMethod]
        public void CalculateSum_MixedOverlap_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] {
                { 1, 5 },   // Начальный сегмент
                { 3, 8 },   // Частично перекрывается с первым
                { 7, 10 },  // Частично перекрывается со вторым
                { 12, 15 }, // Неперекрывающийся
                { 2, 4 }    // Полностью внутри с начальным
            };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            // После сортировки: {1,5}, {2,4}, {3,8}, {7,10}, {12,15}
            // Результат: (5-1) + (8-5) + (10-8) + (15-12) = 4 + 3 + 2 + 3 = 12
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void CalculateSum_LargeIntegerValues_ReturnsCorrectSum()
        {
            // Arrange
            int[,] coordinates = new int[,] { { -1000000000, -1 }, { 0, 1000000000 } };
            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            // Используем длинную арифметику для вычисления ожидаемого результата
            long calcSum = (long)(-1 - (-1000000000)) + (long)(1000000000 - 0);
            Assert.AreEqual((int)calcSum, result);
        }

        [TestMethod]
        public void CalculateSum_MinMaxIntegerBoundary_HandlesSeparately()
        {
            // Arrange - используем отдельные тесты для Min и Max, чтобы избежать переполнения
            int[,] coordinatesMin = new int[,] { { int.MinValue, -1000000000 } };
            int[,] coordinatesMax = new int[,] { { 1000000000, int.MaxValue } };

            ShadowLine shadowLineMin = new ShadowLine(coordinatesMin);
            ShadowLine shadowLineMax = new ShadowLine(coordinatesMax);

            // Act
            int resultMin = shadowLineMin.CalculateSum();
            int resultMax = shadowLineMax.CalculateSum();

            // Assert
            Assert.AreEqual(-1000000000 - int.MinValue, resultMin);
            Assert.AreEqual(int.MaxValue - 1000000000, resultMax);
        }

        [TestMethod]
        public void CalculateSum_LargeNumberOfSegments_ReturnsCorrectSum()
        {
            // Arrange
            int segmentCount = 100;
            int[,] coordinates = new int[segmentCount, 2];

            // Создание непересекающихся сегментов
            for (int i = 0; i < segmentCount; i++)
            {
                coordinates[i, 0] = i * 3;
                coordinates[i, 1] = i * 3 + 2;
            }

            ShadowLine shadowLine = new ShadowLine(coordinates);

            // Act
            int result = shadowLine.CalculateSum();

            // Assert
            // Каждый сегмент имеет длину 2, и есть сегменты segmentCount
            Assert.AreEqual(segmentCount * 2, result);
        }
    }
}
