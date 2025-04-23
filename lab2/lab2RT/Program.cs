namespace lab2RT
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var loginController = new Controller.LoginController();
                var username = loginForm.Controls["usernameTextBox"].Text;
                var mainController = new Controller.MainController(loginController.Users, username);
                var mainForm = new MainForm(mainController);
                mainForm.ShowDialog();
            }
        }
    }
}