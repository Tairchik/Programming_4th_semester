using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class TestClass
    {
        private IVirtualMemory<int> virtualMemoryInteger;
        private IVirtualMemory<string> virtualMemoryChar;
        private IVirtualMemory<string> virtualMemoryString;
        private string command;
        private string typeWorking;
        public TestClass() 
        {
            Start();  
        }

        private void Start()
        {
            PrintInfo();
            bool fl = false;
            while (true) 
            {
                try
                {
                    Console.Write("VM>");
                    command = Console.ReadLine();
                    string nameCommand = command.Split(' ')[0];
                    if (nameCommand.ToLower() == "create")
                    {
                        if (!fl)
                        {
                            Create();
                            fl = true;
                        }
                        else
                        {
                            Console.WriteLine("Сначала необходимо завершить работу с текущим файлом.");
                        }
                    }
                    else if (nameCommand.ToLower() == "open")
                    {
                        if (!fl)
                        {
                            Open();
                            fl = true;
                        }
                        else
                        {
                            Console.WriteLine("Сначала необходимо завершить работу с текущим файлом.");
                        }
                    }
                    else if (nameCommand.ToLower() == "input")
                    {
                        if (fl)
                        {
                            Input();
                        }
                        else
                        {
                            Console.WriteLine("Сначала выполните команду Create/Open");
                        }

                    }
                    else if (nameCommand.ToLower() == "print")
                    {
                        if (fl)
                        {
                            Print();
                        }
                        else
                        {
                            Console.WriteLine("Сначала выполните команду Create/Open");
                        }
                    }
                    else if (nameCommand.ToLower() == "save")
                    {
                        if (fl)
                        {
                            Save();
                        }
                        else
                        {
                            Console.WriteLine("Сначала выполните команду Create/Open");
                        }
                    }
                    else if (nameCommand.ToLower() == "close")
                    {
                        if (fl)
                        {
                            Exit();
                            fl = false;
                        }
                        else
                        {
                            Console.WriteLine("Сначала выполните команду Create/Open");
                        }
                    }
                    else if (nameCommand.ToLower() == "exit")
                    {
                        if (fl)
                        {
                            Exit();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (nameCommand.ToLower() == "help")
                    {
                        PrintInfo();
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод команды. Введите еще раз.");
                        Console.WriteLine("Help - вывести список команд.");
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void PrintInfo()
        {
            Console.WriteLine("Список команд:");
            Console.WriteLine("\tCreate имя файла (int | char(длина строки) | varchar(максимальная длина строки)) размер");
            Console.WriteLine("\tOpen имя файла (int | char(длина строки) | varchar(максимальная длина строки)) размер");
            Console.WriteLine("\tInput (индекс, значение)");
            Console.WriteLine("\tPrint (индекс)");
            Console.WriteLine("\tSave");
            Console.WriteLine("\tClose");
            Console.WriteLine("\tExit");
            Console.WriteLine("\tHelp");
        }

        private void Create()
        {
            // Разбиваем команду на части
            string[] parts = command.Split(' ');

            // Проверяем, что команда содержит правильное количество аргументов
            if (parts.Length != 4)
            {
                throw new Exception("Некорректный ввод строки. Формат: Create имя_файла (тип(параметры)) размер");
            }

            string fileName = parts[1];
            string type = parts[2];
            int size;

            // Проверяем, что размер является корректным числом
            if (!int.TryParse(parts[3], out size) || size <= 0)
            {
                throw new ArgumentException("Некорректный ввод размера. Размер должен быть положительным числом.");
            }

            if (File.Exists($"../../Data/{fileName}.bin")) { throw new Exception("Файл уже существует. Его необходимо открыть."); }

            // Обрабатываем тип данных
            if (type.Split('(')[1].ToLower() == "int)")
            {
                typeWorking = "int";
                virtualMemoryInteger = new VirtualMemoryInteger(fileName, size);
            }
            else if (type.Split('(')[1].ToLower() == "char")
            {
                typeWorking = "char";
                int length;
                if (!int.TryParse(type.Split('(')[2].Substring(0, type.Split('(')[2].Length - 2), out length))
                {
                    throw new ArgumentException("Некорректный ввод длины строки.");
                }
                virtualMemoryChar = new VirtualMemoryChar(fileName, size, length);
            }
            else if (type.Split('(')[1].ToLower() == "varchar")
            {
                typeWorking = "varchar";
                int length;
                if (!int.TryParse(type.Split('(')[2].Substring(0, type.Split('(')[2].Length - 2), out length))
                {
                    throw new ArgumentException("Некорректный ввод длины строки.");
                }
                virtualMemoryString = new VirtualMemoryString(fileName, size, length);
            }
            else
            {
                throw new Exception("Некорректный ввод команды.");
            }
        }

        private void Open()
        {
            // Разбиваем команду на части
            string[] parts = command.Split(' ');

            // Проверяем, что команда содержит правильное количество аргументов
            if (parts.Length != 4)
            {
                throw new Exception("Некорректный ввод строки. Формат: Open имя_файла (тип(параметры)) размер");
            }

            string fileName = parts[1];
            string type = parts[2];
            int size;

            // Проверяем, что размер является корректным числом
            if (!int.TryParse(parts[3], out size) || size <= 0)
            {
                throw new ArgumentException("Некорректный ввод размера. Размер должен быть положительным числом.");
            }

            if (!File.Exists($"../../Data/{fileName}.bin")) { throw new Exception("Файла не существует. Его необходимо создать."); }

            // Обрабатываем тип данных
            if (type.Split('(')[1].ToLower() == "int)")
            {
                typeWorking = "int";
                virtualMemoryInteger = new VirtualMemoryInteger(fileName, size);
            }
            else if (type.Split('(')[1].ToLower() == "char")
            {
                typeWorking = "char";
                int length;
                if (!int.TryParse(type.Split('(')[2].Substring(0, type.Split('(')[2].Length - 2), out length))
                {
                    throw new ArgumentException("Некорректный ввод длины строки.");
                }
                virtualMemoryChar = new VirtualMemoryChar(fileName, size, length);
            }
            else if (type.Split('(')[1].ToLower() == "varchar")
            {
                typeWorking = "varchar";
                int length;
                if (!int.TryParse(type.Split('(')[2].Substring(0, type.Split('(')[2].Length - 2), out length))
                {
                    throw new ArgumentException("Некорректный ввод длины строки.");
                }
                virtualMemoryString = new VirtualMemoryString(fileName, size, length);
            }
            else
            {
                throw new Exception("Некорректный ввод команды.");
            }
        }


        //private void Input()
        //{
        //    if (command.Split(' ').Length != 3)
        //    {
        //        throw new Exception("Некорректный ввод команды.");
        //    }

        //    if (typeWorking == "int")
        //    {
        //        int index;
        //        int value;
        //        if (!int.TryParse(command.Split(' ')[1].Substring(1, command.Split(' ')[1].Length - 2), out index))
        //        {
        //            throw new Exception("Некорректный ввод команды.");
        //        }
        //        if (!int.TryParse(command.Split(' ')[2].Substring(0, command.Split(' ')[2].Length - 1), out value))
        //        {
        //            throw new Exception("Некорректный ввод команды.");
        //        }
        //        if (virtualMemoryInteger.SetElementByIndex(index, value))
        //        {
        //            Console.WriteLine("Замена выполнена.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Замена не выполнена.");
        //        }
        //    }
        //    else if (typeWorking == "char" || typeWorking == "varchar")
        //    {
        //        int index;
        //        string value = command.Split(' ')[2];

        //        // Удаляем лишнюю закрывающую скобку, если она есть
        //        if (value.EndsWith(")"))
        //        {
        //            value = value.Substring(0, value.Length - 1);
        //        }

        //        // Проверка, что значение заключено в кавычки
        //        if (!IsQuoted(value))
        //        {
        //            throw new Exception("Значение должно быть заключено в кавычки.");
        //        }

        //        // Убираем кавычки из значения
        //        value = RemoveQuotes(value);

        //        // Парсим индекс
        //        if (!int.TryParse(command.Split(' ')[1].Substring(1, command.Split(' ')[1].Length - 2), out index))
        //        {
        //            throw new Exception("Некорректный ввод команды.");
        //        }

        //        // Устанавливаем элемент по индексу
        //        if (typeWorking == "char" && virtualMemoryChar.SetElementByIndex(index, value))
        //        {
        //            Console.WriteLine("Замена выполнена.");
        //        }
        //        else if (typeWorking == "varchar" && virtualMemoryString.SetElementByIndex(index, value))
        //        {
        //            Console.WriteLine("Замена выполнена.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Замена не выполнена.");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("Некорректный ввод команды.");
        //    }
        //}

        private void Input()
        {
            // Разбираем команду без потери пробелов внутри кавычек
            string[] parts = ParseCommandWithQuotes(command);

            // Проверяем, что команда содержит правильное количество аргументов
            if (parts.Length != 3)
            {
                throw new Exception("Некорректный ввод команды.");
            }

            int index;
            string value = parts[2];

            // Удаляем лишнюю закрывающую скобку, если она есть
            if (value.EndsWith(")"))
            {
                value = value.Substring(0, value.Length - 1);
            }

            // Проверка, что значение заключено в кавычки (для char и varchar)
            if ((typeWorking == "char" || typeWorking == "varchar") && !IsQuoted(value))
            {
                throw new Exception("Значение должно быть заключено в кавычки.");
            }

            // Убираем кавычки из значения
            if (typeWorking == "char" || typeWorking == "varchar")
            {
                value = RemoveQuotes(value);
            }

            // Парсим индекс
            if (!int.TryParse(parts[1].Substring(1, parts[1].Length - 2), out index))
            {
                throw new Exception("Некорректный ввод команды.");
            }

            // Устанавливаем элемент по индексу
            if (typeWorking == "int")
            {
                int intValue;
                if (!int.TryParse(value, out intValue))
                {
                    throw new Exception("Некорректный ввод значения для типа int.");
                }
                if (virtualMemoryInteger.SetElementByIndex(index, intValue))
                {
                    Console.WriteLine("Замена выполнена.");
                }
                else
                {
                    Console.WriteLine("Замена не выполнена.");
                }
            }
            else if (typeWorking == "char")
            {
                if (virtualMemoryChar.SetElementByIndex(index, value))
                {
                    Console.WriteLine("Замена выполнена.");
                }
                else
                {
                    Console.WriteLine("Замена не выполнена.");
                }
            }
            else if (typeWorking == "varchar")
            {
                if (virtualMemoryString.SetElementByIndex(index, value))
                {
                    Console.WriteLine("Замена выполнена.");
                }
                else
                {
                    Console.WriteLine("Замена не выполнена.");
                }
            }
            else
            {
                throw new Exception("Некорректный ввод команды.");
            }
        }
        private void Print()
        {
            if (command.Split(' ').Length != 2)
            {
                throw new Exception("Некорректный ввод команды.");
            }
            long index;
            if (!long.TryParse(command.Split(' ')[1].Substring(1, command.Split(' ')[1].Length - 2), out index))
            {
                throw new Exception("Некорректный ввод команды.");
            }
            if (typeWorking == "int")
            {
                try
                {
                    Console.WriteLine(virtualMemoryInteger.GetElementByIndex(index));
                }
                catch (ArgumentException e) 
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (typeWorking == "char")
            {
                Console.WriteLine(virtualMemoryChar.GetElementByIndex(index));
            }
            else if (typeWorking == "varchar")
            {
                Console.WriteLine(virtualMemoryString.GetElementByIndex(index));
            }
            else
            {
                throw new Exception("Некорректный ввод команды.");
            }
        }

        private void Exit()
        {
            if (typeWorking == "int")
            {
                virtualMemoryInteger.DumpBuffer();
                virtualMemoryInteger.Close();
            }
            else if (typeWorking == "char")
            {
                virtualMemoryChar.DumpBuffer();
                virtualMemoryChar.Close();
            }
            else if (typeWorking == "varchar")
            {
                virtualMemoryString.DumpBuffer();
                virtualMemoryString.Close();
            }
            else
            {
                throw new Exception("Некорректный ввод команды.");
            }
        }

        private void Save()
        {
            if (typeWorking == "int")
            {
                virtualMemoryInteger.DumpBuffer();
            }
            else if (typeWorking == "char")
            {
                virtualMemoryChar.DumpBuffer();
            }
            else if (typeWorking == "varchar")
            {
                virtualMemoryString.DumpBuffer();
            }
            else
            {
                throw new Exception("Некорректный ввод команды.");
            }
        }

        // Метод для проверки, заключена ли строка в кавычки
        private static bool IsQuoted(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2)
            {
                return false;
            }

            char firstChar = value[0];
            char lastChar = value[value.Length - 1];

            // Проверяем, что строка начинается и заканчивается одинаковыми кавычками
            return (firstChar == '"' && lastChar == '"') || (firstChar == '\'' && lastChar == '\'');
        }

        // Метод для удаления кавычек из строки
        private static string RemoveQuotes(string value)
        {
            if (IsQuoted(value))
            {
                return value.Substring(1, value.Length - 2);
            }

            return value; // Если кавычки отсутствуют, возвращаем исходную строку
        }

        private string[] ParseCommandWithQuotes(string command)
        {
            List<string> parts = new List<string>();
            bool inQuotes = false;
            StringBuilder currentPart = new StringBuilder();

            foreach (char c in command)
            {
                if (c == '"' || c == '\'')
                {
                    inQuotes = !inQuotes; // Переключаем состояние "внутри кавычек"
                    currentPart.Append(c); // Добавляем кавычку в текущую часть
                }
                else if (c == ' ' && !inQuotes)
                {
                    // Если пробел вне кавычек, завершаем текущую часть
                    if (currentPart.Length > 0)
                    {
                        parts.Add(currentPart.ToString());
                        currentPart.Clear();
                    }
                }
                else
                {
                    // Добавляем символ в текущую часть
                    currentPart.Append(c);
                }
            }

            // Добавляем последнюю часть, если она есть
            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString());
            }

            return parts.ToArray();
        }
    }
}
