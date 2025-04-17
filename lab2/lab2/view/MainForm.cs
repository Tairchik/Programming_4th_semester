using System.Windows.Forms;
using System.Globalization;
using MenuLibrary;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AuthorizationLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using lab2LT.Controller;

namespace lab2
{
    public partial class MainForm : Form
    {
        
        private MainController _controller;

        public MainForm(MainController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.AddMenuItems(menuStrip);
            _controller.MenuItemClick += MenuItemClick;
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString(), "Обработчик меню");
        }

    }
}
