using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lab3
{
    public class Server
    {
        private readonly TcpListener _server;
        private bool _isRunning;
        private bool connected = false;
        public Server()
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, 8888);
        }

        public void Start()
        {
            _server.Start();
            _isRunning = true;
            TcpClient client;
            NetworkStream stream;
            
            while (_isRunning)
            {
                client = _server.AcceptTcpClient();
                connected = true;
                stream = client.GetStream();
                // При установлении соединения передаем список логических устройств
                SendDrives(stream);
                while (connected)
                {
                    HandleClient(stream);
                }
                client.Close();
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _server.Stop();
        }

        private void SendDrives(NetworkStream stream)
        {
            string drives = string.Join(",", Directory.GetLogicalDrives());
            SendResponse(stream, drives);
        }

        private void HandleClient(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead;
            if (stream.DataAvailable)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    // Client disconnected
                    return;
                }

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Получен запрос: {request}");

                if (request == "?Disconnect")
                {
                    connected = false;
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
            string[] st = Directory.GetFileSystemEntries(path);

            return string.Join("|", st);
        }

        private void SendResponse(NetworkStream stream, string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                byte[] empty = new byte[] { 0 };
                stream.Write(empty, 0, 1);
                Console.WriteLine($"Отправлен ответ: {Encoding.UTF8.GetString(empty)}");
            }
            else
            {
                byte[] data = Encoding.UTF8.GetBytes(response);
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Отправлен ответ: {Encoding.UTF8.GetString(data)}");
            }
            
        }

    }
}
