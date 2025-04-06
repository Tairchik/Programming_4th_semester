namespace Autorization
{
    using System.Collections.Generic;
    using System.IO;

    namespace AuthorizationLibrary
    {
        public class Authorization
        {
            private readonly string usersFileName;
            private readonly Dictionary<string, User> users = new();

            // Внутренний класс для хранения данных пользователя
            private class User
            {
                public string Password { get; set; }
                public Dictionary<string, int> MenuStatus { get; set; } = new Dictionary<string, int>();
            }

            // Конструктор класса
            public Authorization(string fileName = "USERS.txt")
            {
                usersFileName = fileName;
                LoadUsers();
            }

            // Метод для загрузки данных пользователей из файла
            private void LoadUsers()
            {
                if (!File.Exists(usersFileName))
                {
                    throw new FileNotFoundException("Файл пользователей не найден.", usersFileName);
                }

                string[] lines = File.ReadAllLines(usersFileName);
                User currentUser = null;

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Если строка начинается с '#', это новый пользователь
                    if (line.StartsWith("#"))
                    {
                        var parts = line.Substring(1).Split(' ');
                        if (parts.Length < 2) continue;

                        string username = parts[0];
                        string password = parts[1];

                        currentUser = new User { Password = password };
                        users[username] = currentUser;
                    }
                    else
                    {
                        // Это данные о пункте меню
                        var parts = line.Split(' ');
                        if (parts.Length < 2 || currentUser == null) continue;

                        string menuItem = parts[0];
                        int status = int.Parse(parts[1]);

                        currentUser.MenuStatus[menuItem] = status;
                    }
                }
            }

            // Метод для проверки логина и пароля
            public bool Authenticate(string username, string password)
            {
                if (users.TryGetValue(username, out var user))
                {
                    return user.Password == password;
                }
                return false;
            }

            // Метод для получения статуса пункта меню для пользователя
            public int GetMenuStatus(string username, string menuItem)
            {
                if (users.TryGetValue(username, out var user))
                {
                    if (user.MenuStatus.TryGetValue(menuItem, out int status))
                    {
                        return status;
                    }
                }
                return 0; // По умолчанию, если пункт не указан, он виден и доступен
            }
        }
    }
}