using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_3
{
    public interface IProcessController
    {
        public double PressureInAtm { get; set; }
        public double TemperatureInCelsius { get; set; }
    }
}
