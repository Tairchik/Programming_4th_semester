namespace lab4
{
    public class ShadowLine
    {
        private int[,] _coordinates;
        
        public ShadowLine(int[,] coordinates)
        {
            Coordinates = coordinates;        
        }

        public int CalculateSum()
        {
            // Проверка на пустой массив
            if (Coordinates.GetLength(0) == 0)
            {
                return 0;
            }

            int sum = 0;
            int currentLeft = Coordinates[0, 0];  // Левая граница текущего объединенного отрезка
            int currentRight = Coordinates[0, 1]; // Правая граница текущего объединенного отрезка

            for (int i = 1; i < Coordinates.GetLength(0); i++)
            {
                // Если текущая левая граница больше правой границы объединенного отрезка,
                // добавляем длину текущего объединенного отрезка и начинаем новый
                if (Coordinates[i, 0] > currentRight)
                {
                    sum += currentRight - currentLeft; // Добавляем длину текущего объединенного отрезка
                    currentLeft = Coordinates[i, 0];   // Новая левая граница
                    currentRight = Coordinates[i, 1];  // Новая правая граница
                }
                // Если текущая правая граница больше правой границы объединенного отрезка,
                // расширяем объединенный отрезок вправо
                else if (Coordinates[i, 1] > currentRight)
                {
                    currentRight = Coordinates[i, 1];
                }
                // Если весь текущий отрезок полностью содержится в объединенном отрезке,
                // ничего делать не нужно
            }

            // Добавляем длину последнего объединенного отрезка
            sum += currentRight - currentLeft;

            return sum;
        }

        public int[,] Coordinates
        {
            get
            {
                return _coordinates;
            }
            private set
            {
                if (value.GetLength(1) != 2)
                {
                    throw new ArgumentException("Координаты задаются двумя значениями: левая граница и правая граница.");
                }
                else if (CheckElements(value) == false)
                {
                    throw new ArgumentException("Левая граница отрезка не может быть больше правой границы отрезка.");
                }
                else
                {
                    _coordinates = SortElements(value);
                }
            }
        }
        private bool CheckElements(int[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] > data[i, 1])
                {
                    return false;
                }
            }
            return true;
        }

        private int[,] SortElements(int[,] data)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            // Создаем временный массив для строк
            int[] tempRow = new int[cols];

            // Простая пузырьковая сортировка по первому столбцу (левой границе)
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = i + 1; j < rows; j++)
                {
                    if (data[i, 0] > data[j, 0]) // Сравниваем только левые границы
                    {
                        // Меняем строки местами
                        for (int k = 0; k < cols; k++)
                        {
                            tempRow[k] = data[i, k];
                            data[i, k] = data[j, k];
                            data[j, k] = tempRow[k];
                        }
                    }
                }
            }

            return data;
        }
    }
}
