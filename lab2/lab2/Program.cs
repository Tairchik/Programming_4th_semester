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
                // ������� ������ ����
                var menu = new Menu("..\\..\\..\\..\\menu.txt");

                // ������� ����
                Console.WriteLine("��������� ����:");   
                menu.PrintMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
            }

            try
            {
                // ������� ������ �����������
                var auth = new Authorization("..\\..\\..\\..\\USERS.txt");

                // �������� �����������
                Console.WriteLine("������� ��� ������������:");
                string username = Console.ReadLine();

                Console.WriteLine("������� ������:");
                string password = Console.ReadLine();

                if (auth.Authenticate(username, password))
                {
                    Console.WriteLine("����������� �������!");

                    // �������� ������ ������ ����
                    Console.WriteLine("������� �������� ������ ����:");
                    string menuItem = Console.ReadLine();

                    int status = auth.GetMenuStatus(username, menuItem);
                    switch (status)
                    {
                        case 0:
                            Console.WriteLine("����� ����� � ��������.");
                            break;
                        case 1:
                            Console.WriteLine("����� �����, �� ����������.");
                            break;
                        case 2:
                            Console.WriteLine("����� �� �����.");
                            break;
                        default:
                            Console.WriteLine("����������� ������.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("�������� ��� ������������ ��� ������.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
            }
            */
        }
    }
}