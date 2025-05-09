﻿using System;
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
            int temperature;
            double pressure;
            string data;
            while (_isRunning)
            {
                client = _server.AcceptTcpClient();
                connected = true;
                stream = client.GetStream();
                DateTime dateTime = DateTime.Now;

                while (connected)
                {
                    if ((DateTime.Now - dateTime).TotalSeconds < 1)
                    {
                        HandleClient(stream);
                    }
                    else
                    {
                        dateTime = DateTime.Now;
                        // Генерация значений температуры и давления
                        temperature = _random.Next(0, 101); // 0 - 100 °C
                        pressure = _random.NextDouble() * 6; // 0 - 6 атм

                        // Формирование строки данных
                        data = $"{temperature};{pressure:F2}";

                        SendResponse(stream, data);
                    }
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
