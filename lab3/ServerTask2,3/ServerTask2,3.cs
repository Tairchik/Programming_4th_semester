using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTask2_3
{
    internal class ServerTask2_3
    {
        private readonly TcpListener _server;
        private bool _isRunning;
        private bool connected = false;
        private readonly Random _random;
        public ServerTask2_3()
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, 8888);
            _random = new Random();
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

        private void HandleClient(NetworkStream stream)
        {
            byte[] buffer = new byte[20];
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
                    return;
                }
            }

            // Генерация значений температуры и давления
            int temperature = _random.Next(0, 101); // 0 - 1000 °C
            double pressure = _random.NextDouble() * 6; // 0 - 6 атм

            // Формирование строки данных
            string data = $"{temperature};{pressure:F2}";

            SendResponse(stream, data);

            Thread.Sleep(1000);

        }

        private void SendResponse(NetworkStream stream, string response)
        {
            byte[] data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"Отправлен ответ: {Encoding.UTF8.GetString(data)}");
        }
    }
}
