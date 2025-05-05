using System;
using System.Windows.Forms;
using ScottPlot;

namespace lab3_2Client
{
    public partial class Graph : Form
    {
        public Graph()
        {
            InitializeComponent();
            // Инициализатор
            Temperature.Plot.Title("Датчик температуры");
            Temperature.Plot.XLabel("Время (сек.)");
            Temperature.Plot.YLabel("Температура (цел.)");

            Pressure.Plot.Title("Датчик давления");
            Pressure.Plot.XLabel("Время (сек.)");
            Pressure.Plot.YLabel("Давление (атм.)");
        }
    }
}
