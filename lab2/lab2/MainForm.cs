using System.Windows.Forms;
using System.Globalization;
using Autorization.AuthorizationLibrary;
using MenuLibrary.MenuLibrary;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab2
{
    public partial class MainForm : Form
    {
        private const string pathMenu = "..\\..\\..\\..\\menu.txt";
        private MenuLibrary.MenuLibrary.Menu menu;
        private Authorization auth;
        private string username;

        public MainForm(Authorization auth, string username)
        {
            this.username = username;
            this.auth = auth;
            InitializeComponent();
        }

    }
}
