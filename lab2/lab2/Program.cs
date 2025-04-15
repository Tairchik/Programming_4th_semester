namespace lab2
{
    internal static class Program
    {
        static void Main()  
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}