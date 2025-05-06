using System.Configuration;
using System.Globalization;

namespace ServerTask4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string numberOfInstallationsString = ConfigurationManager.AppSettings["numberOfInstallations"];
            int numberOfInstallations = int.Parse(numberOfInstallationsString);
            ServerTask4 server = new ServerTask4(numberOfInstallations);
            try
            {
                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}