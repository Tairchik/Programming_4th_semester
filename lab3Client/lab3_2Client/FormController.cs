using lab3Client;

namespace lab3_2Client
{
    internal class FormController
    {
        private Client client;
        public event Action<string> Errors;
        public event Action<int, double> DataUpdated;

        public FormController()
        {

        }

        public void StartGetData()
        {
            byte[] buffer = new byte[50];
            while (client.Connected)
            {
                // Получение данных
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Errors?.Invoke("Соединение закрыто сервером.");
                    break;
                }

                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Разбор данных
                string[] values = data.Split(';');
                if (values.Length == 2 && int.TryParse(values[0], out int temperature) && double.TryParse(values[1], out double pressure))
                { 
                    // Отображение графиков
                    DataUpdated?.Invoke(temperature, pressure);
                }
                else
                {
                    Errors?.Invoke("Ошибка при разборе данных.");
                }
            }
        }

        public void ConnectToServer(string IP)
        {
            try
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Close();
                    }
                }

                client = new Client(IP);
                client.Connect();

                return client.GetResponce().Split(',');
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
            }
        }
    }
}
