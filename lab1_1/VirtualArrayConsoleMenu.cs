using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    internal class VirtualArrayConsoleMenu
    {
        private dynamic virtualArray = null;
        private string currentType = null;

        public VirtualArrayConsoleMenu()
        {
            Run();
        }
        public void Run()
        {
            PrintHelp();
            bool exit = false;
            while (!exit)
            {
                Console.Write("> ");
                string inputLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputLine))
                    continue;

                // Определяем команду и аргументы
                string[] parts = inputLine.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0].ToLowerInvariant();

                try
                {
                    switch (command)
                    {
                        case "create":
                            ProcessCreateCommand(parts);
                            break;
                        case "input":
                            ProcessInputCommand(inputLine);
                            break;
                        case "print":
                            ProcessPrintCommand(inputLine);
                            break;
                        case "exit":
                            ProcessExitCommand();
                            exit = true;
                            break;
                        case "help":
                            PrintHelp();
                            break;
                        default:
                            Console.WriteLine("Неизвестная команда. Введите help для справки.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("Команды:");
            Console.WriteLine("  Create <имя файла> <тип>[(параметр)]");
            Console.WriteLine("      Тип: int | char(длина строки) | varchar(максимальная длина строки)");
            Console.WriteLine("  Input (<индекс>, <значение>) – записывает значение в элемент массива (строковое значение в кавычках)");
            Console.WriteLine("  Print (<индекс>) – выводит значение элемента массива");
            Console.WriteLine("  Exit – завершает работу (файлы не удаляются)");
            Console.WriteLine();
        }

        private void ProcessCreateCommand(string[] parts)
        {
            if (parts.Length < 3)
            {
                Console.WriteLine("Некорректный формат команды Create.");
                return;
            }
            string fileName = parts[1];
            string typeSpec = parts[2].Trim();

            if (typeSpec.StartsWith("int", StringComparison.OrdinalIgnoreCase))
            {
                currentType = "int";
                // В данном примере размер массива фиксирован – 50000 элементов, буфер страниц = 3
                virtualArray = new VirtualIntArray(fileName, 50000, 3);
                Console.WriteLine("Виртуальный массив int создан.");
            }
            else if (typeSpec.StartsWith("char", StringComparison.OrdinalIgnoreCase))
            {
                int start = typeSpec.IndexOf('(');
                int end = typeSpec.IndexOf(')');
                if (start < 0 || end < 0 || end <= start + 1)
                {
                    Console.WriteLine("Некорректный формат типа char. Пример: char(10)");
                    return;
                }
                string lengthStr = typeSpec.Substring(start + 1, end - start - 1);
                if (!int.TryParse(lengthStr, out int strLength))
                {
                    Console.WriteLine("Некорректная длина для char.");
                    return;
                }
                currentType = "char";
                virtualArray = new VirtualFixedStringArray(fileName, 50000, strLength, 3);
                Console.WriteLine($"Виртуальный массив char с длиной строки {strLength} создан.");
            }
            else if (typeSpec.StartsWith("varchar", StringComparison.OrdinalIgnoreCase))
            {
                int start = typeSpec.IndexOf('(');
                int end = typeSpec.IndexOf(')');
                if (start < 0 || end < 0 || end <= start + 1)
                {
                    Console.WriteLine("Некорректный формат типа varchar. Пример: varchar(50)");
                    return;
                }
                string lengthStr = typeSpec.Substring(start + 1, end - start - 1);
                if (!int.TryParse(lengthStr, out int maxStrLength))
                {
                    Console.WriteLine("Некорректная максимальная длина для varchar.");
                    return;
                }
                currentType = "varchar";
                // Для varchar используется два файла: swap-файл и файл записей.
                string recordFileName = fileName + "_records.dat";
                virtualArray = new VirtualVarStringArray(fileName, recordFileName, 50000, maxStrLength, 3);
                Console.WriteLine($"Виртуальный массив varchar с максимальной длиной строки {maxStrLength} создан.");
            }
            else
            {
                Console.WriteLine("Неизвестный тип массива. Допустимые типы: int, char, varchar.");
            }
        }

        private void ProcessInputCommand(string inputLine)
        {
            // Формат команды: Input (<индекс>, <значение>)
            int openParen = inputLine.IndexOf('(');
            int comma = inputLine.IndexOf(',');
            int closeParen = inputLine.IndexOf(')');
            if (openParen < 0 || comma < 0 || closeParen < 0)
            {
                Console.WriteLine("Некорректный формат команды Input.");
                return;
            }
            string indexStr = inputLine.Substring(openParen + 1, comma - openParen - 1).Trim();
            string valueStr = inputLine.Substring(comma + 1, closeParen - comma - 1).Trim();
            if (!long.TryParse(indexStr, out long index))
            {
                Console.WriteLine("Некорректный индекс.");
                return;
            }
            // Если строковое значение обрамлено кавычками, удаляем их.
            if (valueStr.StartsWith("\"") && valueStr.EndsWith("\""))
            {
                valueStr = valueStr.Substring(1, valueStr.Length - 2);
            }
            if (virtualArray == null)
            {
                Console.WriteLine("Сначала выполните команду Create.");
                return;
            }
            if (currentType == "int")
            {
                if (int.TryParse(valueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intVal))
                {
                    if (virtualArray.WriteElement(index, intVal))
                        Console.WriteLine($"Элемент с индексом {index} записан значением {intVal}.");
                    else
                        Console.WriteLine("Ошибка записи элемента.");
                }
                else
                {
                    Console.WriteLine("Некорректное значение для int.");
                }
            }
            else if (currentType == "char")
            {
                if (virtualArray.WriteElement(index, valueStr))
                    Console.WriteLine($"Элемент с индексом {index} записан значением \"{valueStr}\".");
                else
                    Console.WriteLine("Ошибка записи элемента.");
            }
            else if (currentType == "varchar")
            {
                // Для varchar используется специальный метод WriteStringElement.
                if (virtualArray.WriteStringElement(index, valueStr))
                    Console.WriteLine($"Элемент с индексом {index} записан значением \"{valueStr}\".");
                else
                    Console.WriteLine("Ошибка записи элемента.");
            }
        }

        private void ProcessPrintCommand(string inputLine)
        {
            // Формат команды: Print (<индекс>)
            int openParen = inputLine.IndexOf('(');
            int closeParen = inputLine.IndexOf(')');
            if (openParen < 0 || closeParen < 0)
            {
                Console.WriteLine("Некорректный формат команды Print.");
                return;
            }
            string indexStr = inputLine.Substring(openParen + 1, closeParen - openParen - 1).Trim();
            if (!long.TryParse(indexStr, out long index))
            {
                Console.WriteLine("Некорректный индекс.");
                return;
            }
            if (virtualArray == null)
            {
                Console.WriteLine("Сначала выполните команду Create.");
                return;
            }
            if (currentType == "int")
            {
                if (virtualArray.ReadElement(index, out int intVal))
                    Console.WriteLine($"Элемент с индексом {index}: {intVal}");
                else
                    Console.WriteLine("Ошибка чтения элемента.");
            }
            else
            {
                if (virtualArray.ReadElement(index, out string strVal))
                    Console.WriteLine($"Элемент с индексом {index}: \"{strVal}\"");
                else
                    Console.WriteLine("Ошибка чтения элемента.");
            }
        }

        private void ProcessExitCommand()
        {
            if (virtualArray != null)
            {
                virtualArray.Close();
            }
            Console.WriteLine("Файлы закрыты. Завершение работы.");
        }
    }
}