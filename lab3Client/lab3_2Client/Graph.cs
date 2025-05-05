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
            Temperature.Plot.Title("Датчик температуры");
            Temperature.Plot.XLabel("Время (сек.)");
            Temperature.Plot.YLabel("Температура (цел.)");
            Pressure.Plot.XLabel("Время (сек.)");
            Pressure.Plot.YLabel("Давление (атм.)");

            Pressure.Plot.Title("Датчик давления");

            controller.Errors += ShowError;
            controller.DataUpdated += UpdateGraph;
        }

        private void ShowError(string message)
        {
            MessageBox.Show($"Error: {message}");
        }

        private void UpdateGraph(List<double> temperature, List<double> pressure)
        {
            ClearGraph();

            Temperature.Plot.Add.Signal(temperature, 1, ScottPlot.Color.FromColor(System.Drawing.Color.Blue));
            Pressure.Plot.Add.Signal(pressure, 1, ScottPlot.Color.FromColor(System.Drawing.Color.Brown));
            Temperature.Plot.Axes.AutoScale();
            Pressure.Plot.Axes.AutoScale();
            Temperature.Refresh();
            Pressure.Refresh();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            controller.ConnectToServer(tBoxIPAddress.Text);
            controller.StartGetData();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
        }

        private void ResetGraphs_Click(object sender, EventArgs e)
        {
            controller.ClearValues();
            ClearGraph();
        }

        private void ClearGraph()
        {
            Temperature.Plot.Clear();
            Pressure.Plot.Clear();
            Temperature.Refresh();
            Pressure.Refresh();
        }
    }
}
