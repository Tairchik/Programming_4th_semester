﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace lab3Client
{
    public class Client
    {
        private readonly TcpClient _client;
        private readonly IPAddress _address;
        private NetworkStream _stream;

        public bool Connected { get { return _client.Connected; } }

        public Client(string ipString)
        {
            _address = IPAddress.Parse(ipString);
            _client = new TcpClient();
        }

        public void Connect()
        {
            _client.Connect(_address, 8888);
            _stream = _client.GetStream();
        }

        public void Close()
        {
            _stream.Write(Encoding.UTF8.GetBytes("?Disconnect"));
            _stream.Close();
            _client.Close();
        }

        public void SendRequest(string requestMessage)
        {
            var requestData = Encoding.UTF8.GetBytes(requestMessage);
            _stream.Write(requestData);
        }

        public string GetResponce()
        {
            var buffer = new byte[4096];
            var byteCount = _stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, byteCount);
        }
    }
}
