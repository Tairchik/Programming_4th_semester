using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3Client
{
    public partial class Translator : Form
    {
        private TranslatorController fileController = new TranslatorController();

        public Translator()
        {
            InitializeComponent();

            // Подписка на события контроллера
            fileController.DirectoryChanged += FileController_DirectoryChanged;
            fileController.FileSelected += FileController_FileSelected;
        }

        private void Translator_Load(object sender, EventArgs e)
        {
            comboBoxSearch.Items.AddRange(fileController.GetLogicalDrives());
            comboBoxSearch.SelectedIndex = 0;
        }

        private void comboBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDrive = comboBoxSearch.SelectedItem.ToString();
            fileController.GetDirectoryEntries(selectedDrive); // триггер DirectoryChanged
        }

        private void comboBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string path = comboBoxSearch.Text;
            if (Directory.Exists(path))
                fileController.GetDirectoryEntries(path);
        }

        private void listBoxSearch_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxSearch.SelectedItem == null) return;

            string displayName = listBoxSearch.SelectedItem.ToString();
            fileController.OnItemSelected(displayName);
        }

        private void FileController_DirectoryChanged(object sender, string path)
        {
            try
            {
                var entries = fileController.DisplayNameToFullPath.Keys.ToArray();

                listBoxSearch.Items.Clear();
                listBoxSearch.Items.AddRange(entries);

                if (!comboBoxSearch.Items.Contains(path))
                    comboBoxSearch.Items.Add(path);

                comboBoxSearch.Text = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке папки: " + ex.Message);
            }
        }

        private void FileController_FileSelected(object sender, string filePath)
        {
            MessageBox.Show("Это файл: " + filePath);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string ip = textBoxIPAddress.Text.Trim();
            netClient = new NetworkClient();

            if (netClient.Connect(ip))
            {
                string drives = netClient.ReceiveInitialDrives();
                listBoxServerSide.Items.Clear();
                listBoxServerSide.Items.AddRange(drives.Split('\n'));
                MessageBox.Show("Соединение установлено!");
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к серверу.");
            }
        }

        private void buttonSendToServer_Click(object sender, EventArgs e)
        {
            if (listBoxSearch.SelectedItem == null || netClient == null) return;

            string displayName = listBoxSearch.SelectedItem.ToString();
            if (fileController.TryGetFullPath(displayName, out string fullPath))
            {
                string response = netClient.SendPath(fullPath);
                listBoxServerSide.Items.Clear();
                listBoxServerSide.Items.AddRange(response.Split('\n'));
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            netClient?.Disconnect();
            MessageBox.Show("Отключено от сервера.");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
