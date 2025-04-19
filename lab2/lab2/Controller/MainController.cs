using AuthorizationLibrary;
using lab2;
using MenuLibrary;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace lab2LT.Controller
{
    public class MainController
    {
        public event EventHandler<EventArgs> MenuItemClick;

        private MainForm _view;
        private string _userName;
        private readonly Menu _menu;
        private Dictionary<string, AuthorizationLibrary.User> users;
        public MainController(Dictionary<string, AuthorizationLibrary.User> users, string username)
        {
            this.users = users;
            _userName = username;
            _menu = new Menu();
        }

        // Метод для получения статуса пункта меню для пользователя
        public int GetMenuStatus(string username, string menuItem)
        {
            if (users.TryGetValue(username, out var userData))
            {
                if (userData.MenuStatus.TryGetValue(menuItem, out int status))
                {
                    return status;
                }
            }
            return 0; // По умолчанию, если пункт не указан, он виден и доступен
        }

        // Метод для добавления пунктов в форму
        public void AddMenuItems(MenuStrip menuStrip)
        {
            foreach (var item in _menu.GetRootItems())
            {
                menuStrip.Items.Add(CreateMenuItem(item));
            }
        }

        // Рекурсивно создаем пункты меню
        private ToolStripMenuItem CreateMenuItem(MenuItem menuItem)
        {
            int status = GetMenuStatus(_userName, menuItem.Name);

            var menu = new ToolStripMenuItem(menuItem.Name);

            foreach (var subItem in menuItem.SubItems)
            {
                menu.DropDownItems.Add(CreateMenuItem(subItem));
            }

            if (status == 0 && !menuItem.SubItems.Any())
            {
                menu.Click += (sender, args) => MenuItemClick?.Invoke(sender, args);
            }
            if (status == 1)
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
