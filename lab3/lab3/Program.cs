using System.Text;

namespace lab3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Server server = new Server();
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