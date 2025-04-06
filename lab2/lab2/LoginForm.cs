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
                // ������� ������ �����������
                var auth = new Authorization("..\\..\\..\\..\\USERS.txt");

                // �������� �����������
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;

                if (auth.Authenticate(username, password))
                {
                    MessageBox.Show("����������� �������!");
                }
                else
                {
                    MessageBox.Show("�������� ��� ������������ ��� ������.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}");
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
                capsLockLabel.Text = "������� CapsLock ������";
            }
            else
            {
                capsLockLabel.Text = "������� CapsLock �� ������";
            }
        }

        private void Form_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            UpdateLanguageStatus();
        }

        private void UpdateLanguageStatus()
        {
            // �������� �������� (�������� "ru-RU", "en-US")
            var culture = InputLanguage.CurrentInputLanguage.Culture;

            // ����� ������ ��� ����� � EN, RU � �.�.
            string lang = culture.TwoLetterISOLanguageName.ToUpper();

            if (lang == "RU")
            {
                languageLabel.Text = $"���� ����� �������";
            }
            else if (lang == "EN")
            {
                languageLabel.Text = $"���� ����� ����������";

            }
        }

    }
}