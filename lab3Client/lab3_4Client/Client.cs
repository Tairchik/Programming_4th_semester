using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lab3_4Client
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
            var data = Encoding.UTF8.GetBytes(requestMessage);
            _stream.Write(data, 0, data.Length);
        }

        public string GetResponce()
        {
            var buffer = new byte[1000];
            var byteCount = _stream.Read(buffer, 0, buffer.Length);
            if (byteCount == 0) 
            {
                return "";
            }
            return Encoding.UTF8.GetString(buffer, 0, byteCount);
        }
    }
}
