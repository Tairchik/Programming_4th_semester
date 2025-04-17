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
        public List<MenuItem> SubItems { get; set; } = new List<MenuItem>(); // Подменю

        public MenuItem(string name)
        {
            Name = name;
        }

        // Метод для вывода меню (рекурсивно)
        public void PrintMenu(int level = 0)
        {
            Console.WriteLine(new string(' ', level * 2) + Name);
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
        public Menu(string fileName = "..\\..\\..\\..\\menu.txt.")
        {
            menuFileName = fileName;
            LoadMenu();
            rootItems = FilterMenuByAccess(rootItems);
        }

        // Рекурсивно фильтруем пункты меню по уровням доступа
        private List<MenuItem> FilterMenuByAccess(List<MenuItem> items)
        {
            var result = new List<MenuItem>();

            foreach (var item in items)
            {
                var newItem = new MenuItem(item.Name);
                newItem.SubItems.AddRange(FilterMenuByAccess(item.SubItems));
                result.Add(newItem);
            }

            return result;
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

                var newItem = new MenuItem(name);

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