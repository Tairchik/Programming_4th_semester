using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTask4
{
    internal class ServerTask4
    {
        private readonly TcpListener _server;
        private bool _isRunning;
        private bool connected = false;
        private readonly int _numberOfInstallations;
        private string[] unitStates;
        public ServerTask4(int numberOfInstallations)
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, 8888);
            _numberOfInstallations = numberOfInstallations;
            unitStates = new string[numberOfInstallations];
            Array.Fill(unitStates, "WORKING");
        }

        public void Start()
        {
            _server.Start();
            _isRunning = true;
            TcpClient client;
            NetworkStream stream;
            Random _random = new Random();

            bool readyForGenerate = false;
            string data;
            DateTime dateTime;

            while (_isRunning)
            {
                client = _server.AcceptTcpClient();
                connected = true;
                stream = client.GetStream();
                dateTime = DateTime.Now;

                // При установлении соединения передаем количество установок
                SendNumberOfInstallations(stream);

                while (connected)
                {
                    if ((DateTime.Now - dateTime).TotalSeconds < 2)
                    {
                        HandleClient(stream, out readyForGenerate);
                    }
                    else
                    {
                        
                        // Генерация значений
                        GenerateValues();

                        // Формирование строки данных
                        data = string.Join(";", unitStates);

                        SendResponse(stream, data);
                        dateTime = DateTime.Now;
                    }
                }
                client.Close();
            }
        }

        private void GenerateValues()
        {
            Random rnd = new Random();
            double stateProbability;
            double repairProbability;
            for (int i = 0; i < _numberOfInstallations; i++)
            {
                if (unitStates[i] == "WORKING")
                {
                    stateProbability = rnd.NextDouble();
                    if (stateProbability >= 0.8)
                    {
                        unitStates[i] = "FAILURE"; // Переход в аварию
                    }
                }
                else if (unitStates[i] == "FAILURE")
                {
                    unitStates[i] = "REPAIR"; // Переход в ремонт
                }
                else if (unitStates[i] == "REPAIR")
                {
                    // Проверка вероятности восстановления (50%)
                    repairProbability = rnd.NextDouble();
                    if (repairProbability < 0.5)
                    {
                        unitStates[i] = "WORKING"; // Возврат в рабочее состояние
                    }
                }
            }
        }

        private void SendNumberOfInstallations(NetworkStream stream)
        {
            SendResponse(stream, _numberOfInstallations.ToString());
        }

        public void Stop()
        {
            _isRunning = false;
            _server.Stop();
        }

        private void HandleClient(NetworkStream stream, out bool readyForGenerate)
        {
            byte[] buffer = new byte[20];
            int bytesRead;
            readyForGenerate = false;
            string request;
            if (stream.DataAvailable)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    // Client disconnected
                    return;
                }

                request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Получен запрос: {request}");

                if (request == "?Disconnect")
                {
                    connected = false;
                    return;
                }
                else if (request == "?Ready")
                {
                    readyForGenerate = true;
                }
            }
            
           

        }

        private void SendResponse(NetworkStream stream, string response)
        {
            byte[] data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"Отправлен ответ: {Encoding.UTF8.GetString(data)}");
        }
    }
}
