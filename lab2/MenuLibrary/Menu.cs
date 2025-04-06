namespace MenuLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    namespace MenuLibrary
    {
        // Класс для представления пункта меню
        public class MenuItem
        {
            public string Name { get; set; } // Название пункта
            public string Handler { get; set; } // Имя обработчика (если есть)
            public List<MenuItem> SubItems { get; set; } = new List<MenuItem>(); // Подменю

            public MenuItem(string name, string handler = null)
            {
                Name = name;
                Handler = handler;
            }

            // Метод для вывода меню (рекурсивно)
            public void PrintMenu(int level = 0)
            {
                Console.WriteLine(new string(' ', level * 2) + Name + (Handler != null ? $" -> {Handler}" : ""));
                foreach (var subItem in SubItems)
                {
                    subItem.PrintMenu(level + 1);
                }
            }
        }

        // Класс для работы с меню
        public class Menu
        {
            private readonly string menuFileName;
            private readonly List<MenuItem> rootItems = new List<MenuItem>();

            // Конструктор класса
            public Menu(string fileName = "menu.txt")
            {
                menuFileName = fileName;
                LoadMenu();
            }

            // Метод для загрузки данных меню из файла
            private void LoadMenu()
            {
                if (!File.Exists(menuFileName))
                {
                    throw new FileNotFoundException("Файл меню не найден.", menuFileName);
                }

                var lines = File.ReadAllLines(menuFileName);
                var stack = new Stack<(int level, MenuItem item)>();
                stack.Push((level: -1, item: null)); // Корневой элемент

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Используем регулярное выражение для разбора строки
                    var match = Regex.Match(line, @"^(\d+)\s+(.+?)(?:\s+(\S+))?$");
                    if (!match.Success)
                    {
                        throw new FormatException($"Некорректный формат строки: {line}");
                    }

                    int level = int.Parse(match.Groups[1].Value); // Уровень
                    string name = match.Groups[2].Value.Trim();   // Название пункта
                    string handler = match.Groups[3].Value;       // Имя обработчика (если есть)

                    var newItem = new MenuItem(name, string.IsNullOrEmpty(handler) ? null : handler);

                    // Находим родительский элемент
                    while (stack.Peek().level >= level)
                    {
                        stack.Pop();
                    }

                    var parent = stack.Peek().item;
                    if (parent == null)
                    {
                        rootItems.Add(newItem);
                    }
                    else
                    {
                        parent.SubItems.Add(newItem);
                    }

                    // Добавляем текущий элемент в стек
                    stack.Push((level, newItem));
                }
            }

            // Метод для вывода всего меню
            public void PrintMenu()
            {
                foreach (var rootItem in rootItems)
                {
                    rootItem.PrintMenu();
                }
            }

            // Метод для получения корневых пунктов меню
            public List<MenuItem> GetRootItems()
            {
                return rootItems;
            }
        }
    }
}