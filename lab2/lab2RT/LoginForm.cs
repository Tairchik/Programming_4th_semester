using System.Windows.Forms;
using System.Globalization;
using System.Reflection;

namespace lab2RT
{
    public partial class LoginForm : Form
    {
        private const string pathUser = "..\\..\\..\\..\\USERS.txt";
        private const string version = "1.0.0.3";
        private readonly MethodInfo method;
        private readonly object instance;
        public LoginForm()
        {
            try
            {
                Assembly asm = Assembly.LoadFrom("Autorization.dll");
                Type? type = asm.GetType("AuthorizationLibrary.Authorization");
                instance = Activator.CreateInstance(type, pathUser);
                // ������� �����
                method = type.GetMethod("Authenticate");
                if (method == null)
                {
                    Console.WriteLine("Method not found.");
                    return;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            InitializeComponent();
            versionLabel.Text = $"������: {version}";
            UpdateLanguageStatus();
            UpdateCapsLockStatus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
/*            try
            {*/
                // �������� �����������
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                object[] args = { username, password };
                if ((bool)method.Invoke(instance, args))
                {
                    MainForm mainForm = new MainForm(instance, username);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("�������� ��� ������������ ��� ������.");
                }
/*            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show($"������: {ex.Message}");
            }*/
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