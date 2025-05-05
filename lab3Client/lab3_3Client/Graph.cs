namespace lab3_3Client
{
    public partial class Graph : Form
    {
        private FormController controller = new FormController();
        public Graph()
        {
            InitializeComponent();
            chart.Plot.Title("Зависимость давление от температуры");
            chart.Plot.XLabel("Температура (цел.)");
            chart.Plot.YLabel("Давление (атм.)");

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

            //chart.Plot.Add.SignalXY(temperature, pressure, ScottPlot.Color.FromColor(Color.Blue));
            chart.Plot.Add.Signal(pressure, 1, ScottPlot.Color.FromColor(Color.Blue));
            chart.Plot.Axes.AutoScale();
            chart.Refresh();
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
            chart.Plot.Clear();
            chart.Refresh();
        }
    }
}
