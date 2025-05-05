namespace ServerTask2_3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServerTask2_3 server = new ServerTask2_3();
            try
            {
                server.Start();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                server.Stop();
            }
        }
    }
}