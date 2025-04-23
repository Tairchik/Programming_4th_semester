namespace Client 
{
    internal static class Program
    {
        static void Main()
        {
            Client client = new Client("127.0.0.1");
            client.Connect();
            Console.WriteLine(client.SendRequest("123"));
        }
    }
}