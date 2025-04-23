using System.Net;
using System.Net.Sockets;
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
            /*// To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());*/

            string ServerResponseString = "All good!";
            byte[] ServerResponseBytes = Encoding.UTF8.GetBytes(ServerResponseString);

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            var tcpListener = new TcpListener(localAddr, 8888);
            try
            {
                tcpListener.Start();    // ��������� ������
                Console.WriteLine("������ �������. �������� �����������... ");

                while (true)
                {
                    // �������� ����������� � ���� TcpClient
                    using var tcpClient = tcpListener.AcceptTcpClient();
                    Console.WriteLine($"�������� �����������: {tcpClient.Client.RemoteEndPoint}");
                    NetworkStream stream = tcpClient.GetStream();
                    var buffer = new byte[4096];
                    Console.WriteLine("[������] ������ �� �������");
                    var byteCount = stream.Read(buffer, 0, buffer.Length);
                    var request = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    Console.WriteLine($"[������] ������ ������� {request}");
                    stream.Write(ServerResponseBytes, 0, ServerResponseBytes.Length);
                    Console.WriteLine("[������] ����� ��� �������");
                }
            }
            finally
            {
                tcpListener.Stop(); // ������������� ������
            }

        }
    }
}