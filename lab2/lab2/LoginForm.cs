using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms;

namespace lab2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text = "����";
            UpdateLanguageStatus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Here you would add your authentication logic
            MessageBox.Show("����������� ���� � �������...");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.CapsLock)
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

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        private void Form_Activated(object sender, EventArgs e)
        {
       
        }

    }
}