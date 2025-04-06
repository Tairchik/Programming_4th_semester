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
            this.Text = "¬ход";
            UpdateLanguageStatus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Here you would add your authentication logic
            MessageBox.Show("¬ыполн€етс€ вход в систему...");
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
                    capsLockLabel.Text = " лавиша CapsLock нажата";
                }
                else
                {
                    capsLockLabel.Text = " лавиша CapsLock не нажата";
                }
            }
        }

        private void Form_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            UpdateLanguageStatus();
        }
        private void UpdateLanguageStatus()
        {
            // ѕолучаем культуру (например "ru-RU", "en-US")
            var culture = InputLanguage.CurrentInputLanguage.Culture;

            // Ѕерем только две буквы Ч EN, RU и т.д.
            string lang = culture.TwoLetterISOLanguageName.ToUpper();

            if (lang == "RU")
            {
                languageLabel.Text = $"язык ввода –усский";
            }
            else if (lang == "EN")
            {
                languageLabel.Text = $"язык ввода јнглийский";

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