using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace lab2RT
{
    public partial class MainForm : Form
    {
        private readonly object menuInstance; // Экземпляр MenuLibrary.Menu
        private readonly object auth; // Объект из загруженной DLL Authorization
        private readonly string username;

        public MainForm(object auth, string username)
        {
            this.username = username;
            this.auth = auth;

            // Загружаем MenuLibrary.dll
            Assembly menuAssembly = Assembly.LoadFrom("MenuLibrary.dll");
            Type menuType = menuAssembly.GetType("MenuLibrary.Menu");

            if (menuType == null)
            {
                throw new InvalidOperationException("Класс MenuLibrary.Menu не найден.");
            }

            // Создаем экземпляр MenuLibrary.Menu
            ConstructorInfo menuConstructor = menuType.GetConstructor(new[] { typeof(string) });
            if (menuConstructor == null)
            {
                throw new InvalidOperationException("Конструктор MenuLibrary.Menu(string) не найден.");
            }
            menuInstance = menuConstructor.Invoke(new object[] { "..\\..\\..\\..\\menu.txt" });

            InitializeComponent();

            MethodInfo getRootItemsMethod = menuType.GetMethod("GetRootItems");
            if (getRootItemsMethod == null)
            {
                throw new InvalidOperationException("Метод GetRootItems не найден.");
            }

            // Получаем список пунктов меню
            var rootItems = (IEnumerable)getRootItemsMethod.Invoke(menuInstance, null);

            // Преобразуем в List<object>
            var rootItemsList = new List<object>();
            foreach (var item in rootItems)
            {
                rootItemsList.Add(item);
            }

            // Фильтруем пункты меню по уровням доступа
            var filteredItems = FilterMenuByAccess(rootItemsList);

            // Добавляем отфильтрованные пункты в форму
            foreach (var item in filteredItems)
            {
                menuStrip.Items.Add(CreateMenuItem(item));
            }
        }

        // Рекурсивно фильтруем пункты меню по уровням доступа
        private IEnumerable FilterMenuByAccess(IEnumerable items)
        {
            var result = new List<object>();

            foreach (var item in items)
            {
                Type itemType = item.GetType();
                PropertyInfo nameProperty = itemType.GetProperty("Name");
                PropertyInfo subItemsProperty = itemType.GetProperty("SubItems");

                if (nameProperty == null || subItemsProperty == null)
                {
                    throw new InvalidOperationException("Неверная структура пункта меню.");
                }

                string itemName = (string)nameProperty.GetValue(item);
                var subItemsEnumerable = (IEnumerable)subItemsProperty.GetValue(item);

                // Находим конструктор с параметром string
                ConstructorInfo constructor = itemType.GetConstructor(new[] { typeof(string) });
                if (constructor == null)
                {
                    throw new InvalidOperationException("Конструктор с параметром string не найден.");
                }

                // Создаем новый экземпляр, передавая имя пункта меню
                var newItem = constructor.Invoke(new object[] { itemName });

                // Фильтруем подэлементы
                var filteredSubItems = FilterMenuByAccess(subItemsEnumerable);

                // Создаем новый список типа List<MenuLibrary.MenuItem>
                var subItemsList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

                foreach (var subItem in filteredSubItems)
                {
                    subItemsList.Add(subItem); // Добавляем элементы в новый список
                }

                // Устанавливаем отфильтрованные подэлементы
                subItemsProperty.SetValue(newItem, subItemsList);

                result.Add(newItem);
            }

            return result;
        }

        // Рекурсивно создаем пункты меню
        private ToolStripMenuItem CreateMenuItem(object menuItem)
        {
            Type menuItemType = menuItem.GetType();
            PropertyInfo nameProperty = menuItemType.GetProperty("Name");
            PropertyInfo subItemsProperty = menuItemType.GetProperty("SubItems");

            if (nameProperty == null || subItemsProperty == null)
            {
                throw new InvalidOperationException("Неверная структура пункта меню.");
            }

            string itemName = (string)nameProperty.GetValue(menuItem);
            var subItemsEnumerable = (IEnumerable)subItemsProperty.GetValue(menuItem);

            // Преобразуем подэлементы в List<object>
            var subItems = new List<object>();
            foreach (var subItem in subItemsEnumerable)
            {
                subItems.Add(subItem);
            }

            // Используем рефлексию для вызова метода GetMenuStatus
            int status = GetMenuStatus(itemName);

            var menu = new ToolStripMenuItem(itemName);

            foreach (var subItem in subItems)
            {
                menu.DropDownItems.Add(CreateMenuItem(subItem));
            }

            if (status == 0 && subItems.Count == 0)
            {
                menu.Click += MenuItemClick;
            }
            else if (status == 1)
            {
                menu.BackColor = Color.LightGray;
                menu.Enabled = false;
            }
            else if (status == 2)
            {
                menu.Visible = false;
            }

            return menu;
        }

        // Метод для вызова GetMenuStatus через рефлексию
        private int GetMenuStatus(string menuItemName)
        {
            Type authType = auth.GetType();
            MethodInfo method = authType.GetMethod("GetMenuStatus");

            if (method != null)
            {
                object[] args = { username, menuItemName };
                return (int)method.Invoke(auth, args);
            }

            throw new InvalidOperationException("Метод GetMenuStatus не найден.");
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString(), "Обработчик меню");
        }
    }
}