using lab3Client;
using System.Text;

namespace lab3_2Client
{
    internal class FormController
    {
        private Client client;
        public event Action<string> Errors;
        public event Action<List<double>, List<double>> DataUpdated;
        private List<double> temps = new();
        private List<double> pressures = new();
        private System.Windows.Forms.Timer timer;
        public FormController()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(DataUpdate);
            timer.Interval = 1000;
        }

        public void StartGetData()
        {
            timer.Start();
        }

        private void DataUpdate(object sender, EventArgs e)
        {
            byte[] buffer = new byte[200];
            if (client.Connected)
            {
                // Получение данных
                string data = client.GetResponce();
                if (string.IsNullOrEmpty(data))
                {
                    Errors?.Invoke("Соединение закрыто сервером.");
                    return;
                }

                // Разбор данных
                string[] values = data.Split(';');
                for (int i = 0; i < values.Length; i+=2)
                {
                    double.TryParse(values[i], out double temperature);
                    double.TryParse(values[i+1], out double pressure);
                    temps.Add(temperature);
                    pressures.Add(pressure);
                }
                // Отображение графиков
                DataUpdated?.Invoke(temps, pressures);
            }
        }

        public void ClearValues()
        {
            temps.Clear();
            pressures.Clear();
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
                        timer.Stop();
                    }
                }

                client = new Client(IP);
                client.Connect();

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
                        timer.Stop();
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
