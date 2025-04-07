using Autorization.AuthorizationLibrary;
using MenuLibrary.MenuLibrary;

namespace lab2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
           


            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());


            /*// To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());*/
            /*
            try
            {
                // Создаем объект меню
                var menu = new Menu("..\\..\\..\\..\\menu.txt");

                // Выводим меню
                Console.WriteLine("Структура меню:");   
                menu.PrintMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            try
            {
                // Создаем объект авторизации
                var auth = new Authorization("..\\..\\..\\..\\USERS.txt");

                // Тестовая авторизация
                Console.WriteLine("Введите имя пользователя:");
                string username = Console.ReadLine();

                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();

                if (auth.Authenticate(username, password))
                {
                    Console.WriteLine("Авторизация успешна!");

                    // Получаем статус пункта меню
                    Console.WriteLine("Введите название пункта меню:");
                    string menuItem = Console.ReadLine();

                    int status = auth.GetMenuStatus(username, menuItem);
                    switch (status)
                    {
                        case 0:
                            Console.WriteLine("Пункт виден и доступен.");
                            break;
                        case 1:
                            Console.WriteLine("Пункт виден, но недоступен.");
                            break;
                        case 2:
                            Console.WriteLine("Пункт не виден.");
                            break;
                        default:
                            Console.WriteLine("Неизвестный статус.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверное имя пользователя или пароль.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            */
        }
    }
}