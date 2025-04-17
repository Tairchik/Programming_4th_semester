using System.Windows.Forms;
using System.Globalization;
using AuthorizationLibrary;
using MenuLibrary;
using lab2LT.Controller;

namespace lab2
{
    public partial class LoginForm : Form
    {
        private const string version = "1.0.0.5";
        private LoginController loginController;
        private KeyController _keyController;

        public LoginForm()
        {
            InitializeComponent();
            versionLabel.Text = $"Версия: {version}";

            loginController = new LoginController(this);
            _keyController = new KeyController();

            _keyController.CapsLockChanged += OnCapsLockChanged;
            _keyController.InputLanguageChanged += OnInputLanguageChanged;
            _keyController.EnterPressed += LoginButton_Click;
            _keyController.CloseRequested += CancelButton_Click;

            _keyController.Initialize();
        }
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            _keyController.OnKeyDown(e.KeyCode);
        }

        private void Form_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            _keyController.OnInputLanguageChanged();
        }

        private void OnCapsLockChanged(object sender, string statusMessage)
        {
            capsLockLabel.Text = statusMessage;
        }

        private void OnInputLanguageChanged(object sender, string lang)
        {
            languageLabel.Text = lang;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                loginController.AuthorizationData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string GetName()
        {
            return usernameTextBox.Text;
        }

        public string GetPassword()
        {
            return passwordTextBox.Text;
        }
    }
}