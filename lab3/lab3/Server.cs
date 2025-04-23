using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Server
    {
        private readonly TcpListener _server;
        private bool _isRunning;
        public Server()
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, 8888);
        }

        public void Start()
        {
            _server.Start();
            _isRunning = true;
            
            while (_isRunning)
            {
                using TcpClient client = _server.AcceptTcpClient();
                HandleClient(client);
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _server.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Получен запрос: {request}");

                if (string.IsNullOrEmpty(request))
                {
                    SendResponse(stream, "Ошибка: пустой запрос");
                    return;
                }

                if (request == "GET_DRIVES")
                {
                    string drives = string.Join(",", Directory.GetLogicalDrives());
                    SendResponse(stream, drives);
                }
                else if (Directory.Exists(request))
                {
                    string directoryStructure = GetDirectoryStructure(request);
                    SendResponse(stream, directoryStructure);
                }
                else if (File.Exists(request))
                {
                    string fileContent = File.ReadAllText(request);
                    SendResponse(stream, fileContent);
                }
                else
                {
                    SendResponse(stream, "Ошибка: указанный путь не существует");
                }
            }
        }

        private string GetDirectoryStructure(string path)
        {
            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);

            return $"Файлы: {string.Join(", ", files)}\nКаталоги: {string.Join(", ", directories)}";
        }

        private void SendResponse(NetworkStream stream, string response)
        {
            byte[] data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);
        }

    }
}
