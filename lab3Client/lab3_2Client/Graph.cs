using System;
using System.Windows.Forms;
using ScottPlot;

namespace lab3_2Client
{
    public partial class Graph : Form
    {
        private FormController controller = new FormController();
        public Graph()
        {
            InitializeComponent();
            controller.Errors += ShowError;
            controller.DataUpdated += UpdateGraph;
        }

        private void ShowError(string message)
        {
            MessageBox.Show($"Error: {message}");
        }

        private void UpdateGraph(List<double> temperature, List<double> pressure)
        {
            Temperature.Plot.Add.Signal(temperature);
            Pressure.Plot.Add.Signal(pressure);
            Temperature.Refresh();
            Pressure.Refresh();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            controller.ConnectToServer("127.0.0.1");
            controller.StartGetData();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
        }
    }
}
