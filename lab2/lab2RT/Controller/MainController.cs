using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace lab2RT.Controller
{
    public class MainController
    {
        private EventHandler<EventArgs> _menuItemClick;

        private readonly object menuInstance; // Instance of MenuLibrary.Menu
        private readonly System.Collections.Generic.Dictionary<string, object> users;
        private readonly string username;

        public event EventHandler<EventArgs> MenuItemClick
        {
            add
            {
                _menuItemClick += value;
            }
            remove
            {
                _menuItemClick -= value;
            }
        }

        public MainController(System.Collections.Generic.Dictionary<string, object> users, string username)
        {
            this.users = users;
            this.username = username;

            // Load MenuLibrary.dll
            Assembly menuAssembly = Assembly.LoadFrom("MenuLibrary.dll");
            Type menuType = menuAssembly.GetType("MenuLibrary.Menu");

            if (menuType == null)
            {
                throw new InvalidOperationException("Class MenuLibrary.Menu not found.");
            }

            // Create instance of MenuLibrary.Menu
            ConstructorInfo menuConstructor = menuType.GetConstructor(new[] { typeof(string) });
            if (menuConstructor == null)
            {
                throw new InvalidOperationException("Constructor MenuLibrary.Menu(string) not found.");
            }
            menuInstance = menuConstructor.Invoke(new object[] { "..\\..\\..\\..\\menu.txt" });
        }

        // Method to get menu status for user and menu item
        public int GetMenuStatus(string menuItem)
        {
            // Use users dictionary to get menu status
            if (users.TryGetValue(username, out var userData))
            {
                dynamic userDynamic = userData;
                try
                {
                    if (userDynamic.MenuStatus.TryGetValue(menuItem, out int status))
                    {
                        return status;
                    }
                }
                catch
                {
                    // If property or method not found, default to 0
                }
            }
            return 0; // Default: visible and enabled
        }

        // Method to add menu items to MenuStrip
        public void AddMenuItems(MenuStrip menuStrip)
        {
            Type menuType = menuInstance.GetType();
            MethodInfo getRootItemsMethod = menuType.GetMethod("GetRootItems");
            if (getRootItemsMethod == null)
            {
                throw new InvalidOperationException("Method GetRootItems not found.");
            }

            var rootItems = (IEnumerable)getRootItemsMethod.Invoke(menuInstance, null);
            var rootItemsList = new List<object>();
            foreach (var item in rootItems)
            {
                rootItemsList.Add(item);
            }

            var filteredItems = FilterMenuByAccess(rootItemsList);

            foreach (var item in filteredItems)
            {
                menuStrip.Items.Add(CreateMenuItem(item));
            }
        }

        // Recursive method to filter menu items by access level
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
                    throw new InvalidOperationException("Invalid menu item structure.");
                }

                string itemName = (string)nameProperty.GetValue(item);
                var subItemsEnumerable = (IEnumerable)subItemsProperty.GetValue(item);

                ConstructorInfo constructor = itemType.GetConstructor(new[] { typeof(string) });
                if (constructor == null)
                {
                    throw new InvalidOperationException("Constructor with string parameter not found.");
                }

                var newItem = constructor.Invoke(new object[] { itemName });

                var filteredSubItems = FilterMenuByAccess(subItemsEnumerable);

                var subItemsList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

                foreach (var subItem in filteredSubItems)
                {
                    subItemsList.Add(subItem);
                }

                subItemsProperty.SetValue(newItem, subItemsList);

                result.Add(newItem);
            }

            return result;
        }

        // Recursive method to create ToolStripMenuItem from menu item object
        private ToolStripMenuItem CreateMenuItem(object menuItem)
        {
            Type menuItemType = menuItem.GetType();
            PropertyInfo nameProperty = menuItemType.GetProperty("Name");
            PropertyInfo subItemsProperty = menuItemType.GetProperty("SubItems");

            if (nameProperty == null || subItemsProperty == null)
            {
                throw new InvalidOperationException("Invalid menu item structure.");
            }

            string itemName = (string)nameProperty.GetValue(menuItem);
            var subItemsEnumerable = (IEnumerable)subItemsProperty.GetValue(menuItem);

            var subItems = new List<object>();
            foreach (var subItem in subItemsEnumerable)
            {
                subItems.Add(subItem);
            }

            int status = GetMenuStatus(itemName);

            var menu = new ToolStripMenuItem(itemName);

            foreach (var subItem in subItems)
            {
                menu.DropDownItems.Add(CreateMenuItem(subItem));
            }

            if (status == 0 && subItems.Count == 0)
            {
                _menuItemClick?.Invoke(menu, EventArgs.Empty);
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
    }
}
