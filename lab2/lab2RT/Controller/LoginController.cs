using System;
using System.Reflection;
using System.Windows.Forms;

namespace lab2RT.Controller
{
    public class LoginController
    {
        private const string pathUser = "..\\..\\..\\..\\USERS.txt";
        private readonly object authInstance;
        private readonly MethodInfo authenticateMethod;
        private string _authenticatedUsername;
        private System.Collections.Generic.Dictionary<string, object> _users;

        public string AuthenticatedUsername => _authenticatedUsername;
        public object AuthInstance => authInstance;
        public System.Collections.Generic.Dictionary<string, object> Users => _users;

        public LoginController()
        {
            try
            {
                var asm = Assembly.LoadFrom("Autorization.dll");
                var type = asm.GetType("AuthorizationLibrary.Authorization");
                authInstance = Activator.CreateInstance(type, pathUser);
                _users = (System.Collections.Generic.Dictionary<string, object>)type.GetProperty("users").GetValue(authInstance);
                authenticateMethod = type.GetMethod("Authenticate");
                if (authenticateMethod == null)
                {
                    throw new InvalidOperationException("Authenticate method not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading authorization: {ex.Message}");
                throw;
            }
        }

        public bool Authenticate(string username, string password)
        {
            object[] args = { username, password };
            bool result = (bool)authenticateMethod.Invoke(authInstance, args);
            if (result)
            {
                _authenticatedUsername = username;
            }
            return result;
        }
    }
}
