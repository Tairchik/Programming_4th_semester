using System.Windows.Forms;
using System.Globalization;
using MenuLibrary;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AuthorizationLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace lab2
{
    public partial class MainForm : Form
    {
        private readonly MenuLibrary.Menu menu;
        private Authorization auth;
        private readonly string username;

        public MainForm(Authorization auth, string username)
        {
            this.username = username;
            this.auth = auth;
            InitializeComponent();

            menu = new MenuLibrary.Menu();
            var rootItems = menu.GetRootItems();
            // Фильтруем пункты меню по уровням доступа
            var filteredItems = FilterMenuByAccess(rootItems);

            // Добавляем отфильтрованные пункты в форму
            foreach (var item in filteredItems)
            {
                menuStrip.Items.Add(CreateMenuItem(item));
            }
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

        // Рекурсивно создаем пункты меню
        private ToolStripMenuItem CreateMenuItem(MenuItem menuItem)
        {
            int status = auth.GetMenuStatus(username, menuItem.Name);

            var menu = new ToolStripMenuItem(menuItem.Name);

            foreach (var subItem in menuItem.SubItems)
            {
                menu.DropDownItems.Add(CreateMenuItem(subItem));
            }

            if (status == 0 && !menuItem.SubItems.Any())
            {
                menu.Click += MenuItemClick;
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

        private void MenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString(), "Обработчик меню");
        }

    }
}
