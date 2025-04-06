namespace lab2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            UpdateCapsLockStatus();
            this.Text = "Вход";
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Here you would add your authentication logic
            MessageBox.Show("Выполняется вход в систему...");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateCapsLockStatus();
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateCapsLockStatus();
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            UpdateCapsLockStatus();
        }

        private void UpdateCapsLockStatus()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsLockLabel.Text = "Клавиша CapsLock нажата";
                capsLockLabel.ForeColor = Color.Red;
            }
            else
            {
                capsLockLabel.Text = "Клавиша CapsLock не нажата";
                capsLockLabel.ForeColor = SystemColors.ControlText;
            }
        }

    }
}