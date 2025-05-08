using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            int currentRight = Coordinates[0, 0]; // Изначально берем левую границу первого отрезка

            for (int i = 0; i < Coordinates.GetLength(0); i++)
            {
                // Если текущая левая граница больше правой границы предыдущего объединенного отрезка,
                // то начинаем новый объединенный отрезок
                if (Coordinates[i, 0] > currentRight)
                {
                    sum += currentRight - Coordinates[0, 0]; // Добавляем длину предыдущего объединенного отрезка
                    Coordinates[0, 0] = Coordinates[i, 0];   // Новая левая граница
                    currentRight = Coordinates[i, 1];        // Новая правая граница
                }
                // Если текущая правая граница больше правой границы текущего объединенного отрезка,
                // расширяем текущий объединенный отрезок вправо
                else if (Coordinates[i, 1] > currentRight)
                {
                    currentRight = Coordinates[i, 1];
                }
                // Если весь текущий отрезок полностью содержится в объединенном отрезке,
                // ничего делать не нужно
            }

            // Добавляем длину последнего объединенного отрезка
            sum += currentRight - Coordinates[0, 0];

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
