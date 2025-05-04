namespace lab3Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*// To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Translator());*/
            Client client = new Client("127.0.0.1");
            client.Connect();
            Console.WriteLine(client.GetResponce());
            client.SendRequest("C:/");
            Console.WriteLine(client.GetResponce());
            client.SendRequest("C:/hello/");
            Console.WriteLine(client.GetResponce());
            client.SendRequest("C:/hello/hello.cpp");
            Console.WriteLine(client.GetResponce());
            Console.WriteLine(client.Connected);
            client.Close();
            Console.WriteLine(client.Connected);
        }
    }
}