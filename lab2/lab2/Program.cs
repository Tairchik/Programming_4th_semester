namespace lab2
{
    internal static class Program
    {
        static void Main()  
        {
            ApplicationConfiguration.Initialize();
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var users = loginForm.Users;
                var username = loginForm.AuthenticatedUsername;
                var mainController = new lab2LT.Controller.MainController(users, username);
                MainForm mainForm = new MainForm(mainController);
                mainForm.ShowDialog();
            }
        }
    }
}