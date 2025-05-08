using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab4
{
    internal class ShadowLine
    {
        private double[,] _coordinates;
        
        public ShadowLine(double[,] coordinates)
        {
            Coordinates = coordinates;        
        }

        public double CalculateSum()
        {
            double sum = Coordinates[0, 1] - Coordinates[0, 0];
            for (int i = 1; i < Coordinates.GetLength(0); i++)
            {
                // Если не пересекаются
                if (Coordinates[i, 0] > Coordinates[i - 1, 1])
                {
                    sum += Coordinates[i, 1] - Coordinates[i, 0];
                }
                // Пересекаются и один не вложен в другого
                else if (Coordinates[i, 1] > Coordinates[i - 1, 1])
                {
                    sum += Coordinates[i, 1] - Coordinates[i - 1, 1];
                }
            }
            return sum;
        }

        public double[,] Coordinates
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
        private bool CheckElements(double[,] data)
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

        private double[,] SortElements(double[,] data)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            // Создаем временный массив для строк
            double[] tempRow = new double[cols];

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
