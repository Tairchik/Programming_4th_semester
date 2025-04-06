using System.Windows.Forms;
using System.Globalization;
using Autorization.AuthorizationLibrary;
using MenuLibrary.MenuLibrary;

namespace lab2
{
    public partial class LoginForm : Form
    {
        private const string pathMenu = "..\\..\\..\\..\\menu.txt";
        private const string pathUser = "..\\..\\..\\..\\USER.txt";

        public LoginForm()
        {
            InitializeComponent();
            UpdateLanguageStatus();
            UpdateCapsLockStatus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем объект авторизации
                var auth = new Authorization("..\\..\\..\\..\\USERS.txt");

                // Тестовая авторизация
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;

                if (auth.Authenticate(username, password))
                {
                    MessageBox.Show("Авторизация успешна!");
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.CapsLock)
            {
                UpdateCapsLockStatus();
            }
        }

        private void UpdateCapsLockStatus()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsLockLabel.Text = "Клавиша CapsLock нажата";
            }
            else
            {
                capsLockLabel.Text = "Клавиша CapsLock не нажата";
            }
        }

        private void Form_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            UpdateLanguageStatus();
        }

        private void UpdateLanguageStatus()
        {
            // Получаем культуру (например "ru-RU", "en-US")
            var culture = InputLanguage.CurrentInputLanguage.Culture;

            // Берем только две буквы — EN, RU и т.д.
            string lang = culture.TwoLetterISOLanguageName.ToUpper();

            if (lang == "RU")
            {
                languageLabel.Text = $"Язык ввода Русский";
            }
            else if (lang == "EN")
            {
                languageLabel.Text = $"Язык ввода Английский";

            }
        }

    }
}