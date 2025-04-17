using AuthorizationLibrary;
using lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2LT.Controller
{
    public class LoginController
    {
        private const string pathUser = "..\\..\\..\\..\\USERS.txt";
        private LoginForm view;
        public LoginController(LoginForm loginForm)
        {
            view = loginForm;
        }

        public void AuthorizationData()
        {
            // Создаем объект авторизации
            var auth = new Authorization(pathUser);

            // Тестовая авторизация
            string username = view.GetName();
            string password = view.GetPassword();

            if (auth.Authenticate(username, password))
            {
                MainForm mainForm = new MainForm(new MainController(auth, username));
                mainForm.Show();
                view.Hide();
            }
            else
            {
                throw new Exception("Неверное имя пользователя или пароль.");
            }
        } 
    }
}
