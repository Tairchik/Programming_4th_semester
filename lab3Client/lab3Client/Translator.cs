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
        private TranslatorController controller = new TranslatorController();

        public Translator()
        {
            InitializeComponent();

            // Подписка на события контроллера
            controller.DirectoryChanged += FileController_DirectoryChanged;
            controller.FileSelected += FileController_FileSelected;
            controller.Errors += ShowError;
        }



        private void comboBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string path = comboBoxSearch.Text;
            controller.GetDirectoryEntries(path);
            fileContent.Text = string.Empty;
        }

        private void SendToServer_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxSearch.SelectedItem == null) return;

            string displayName = listBoxSearch.SelectedItem.ToString();
            controller.OnItemSelected(displayName);
        }

        private void FileController_DirectoryChanged(object sender, string path)
        {
            try
            {
                if (!comboBoxSearch.Items.Contains(path))
                    comboBoxSearch.Items.Add(path);

                comboBoxSearch.Text = path;
                var entries = controller.DisplayNameToFullPath.Keys.ToArray();

                listBoxSearch.Items.Clear();
                listBoxSearch.Items.AddRange(entries);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке папки: " + ex.Message);
            }
        }

        private void FileController_FileSelected(object sender, string filePath)
        {
            fileContent.Text = string.Empty;
            fileContent.Text = controller.GetFileText(filePath);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            listBoxSearch.Items.Clear();
            comboBoxSearch.Items.Clear();
            comboBoxSearch.Text = null;
            fileContent.Text = string.Empty;
            comboBoxSearch.Items.AddRange(controller.ConnectToServer(textBoxIPAddress.Text));
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
            listBoxSearch.Items.Clear();
            comboBoxSearch.Items.Clear();
            comboBoxSearch.Text = null;
            fileContent.Text = string.Empty;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
            this.Close();
        }

        private void FormExit_Click(object sender, FormClosingEventArgs e)
        {
            controller.Disconnect();
        }

        private void ShowError(string message)
        {
            MessageBox.Show($"Error: {message}");
        }
    }
}
