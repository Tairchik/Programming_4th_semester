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
            // �������������
            Temperature.Plot.Title("������ �����������");
            Temperature.Plot.XLabel("����� (���.)");
            Temperature.Plot.YLabel("����������� (���.)");

            Pressure.Plot.Title("������ ��������");
            Pressure.Plot.XLabel("����� (���.)");
            Pressure.Plot.YLabel("�������� (���.)");
        }
    }
}
