using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab3_4Client
{
    internal class PultController
    {
        private Client client;
        public event Action<string> Errors;
        public event Action<List<Color>> DataUpdated;
        private List<Color> buttonsStates;
        private System.Windows.Forms.Timer timer;
        private int buttonNumbers;

        public PultController()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(DataUpdate);
            timer.Interval = 2000;
        }

        public void StartGetData()
        {
            client.SendRequest("?Ready");
            timer.Start();
        }

        private void DataUpdate(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = new byte[1000];
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
                    buttonsStates = new List<Color>(values.Length);
                    for (int i = 0; i < values.Length; i++)
                    {
                        switch (values[i])
                        {
                            case "WORKING":
                                buttonsStates.Add(Color.Green);
                                break;
                            case "FAILURE":
                                buttonsStates.Add(Color.Red);
                                break;
                            case "REPAIR":
                                buttonsStates.Add(Color.Gray);
                                break;
                        }
                    }
                    // Отображение графиков
                    DataUpdated?.Invoke(buttonsStates);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Errors?.Invoke(ex.Message);
            }
        }

        public int ConnectToServer(string IP)
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
                buttonNumbers = int.Parse(client.GetResponce());
                return buttonNumbers;
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
                return 0;
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
