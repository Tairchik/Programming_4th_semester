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
        }

        

        private void comboBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string path = comboBoxSearch.Text;
            controller.GetDirectoryEntries(path);
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
                var entries = controller.DisplayNameToFullPath.Keys.ToArray();

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
            comboBoxSearch.Items.AddRange(controller.ConnectToServer(textBoxIPAddress.Text)); 
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
            this.Close();
        }

        private void menuExit_Click(object sender, FormClosingEventArgs e)
        {
            controller.Disconnect();
        }
    }
}
