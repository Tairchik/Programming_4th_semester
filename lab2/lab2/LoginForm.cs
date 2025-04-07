using System.Windows.Forms;
using System.Globalization;
using AuthorizationLibrary;
using MenuLibrary;

namespace lab2
{
    public partial class LoginForm : Form
    {
        private const string pathUser = "..\\..\\..\\..\\USERS.txt";
        private const string version = "1.0.0.3";

        public LoginForm()
        {
            InitializeComponent();
            versionLabel.Text = $"������: {version}";
            UpdateLanguageStatus();
            UpdateCapsLockStatus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                // ������� ������ �����������
                var auth = new Authorization(pathUser);

                // �������� �����������
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;

                if (auth.Authenticate(username, password))
                {
                    MainForm mainForm = new MainForm(auth, username);
                    mainForm.Show();
                    this.Hide();
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
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                LoginButton_Click(sender, e);
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