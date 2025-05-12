using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_3
{
    public class MicroController
    {
        public double PressureInPascals { get; private set; }
        public double TemperatureInFahrenheit { get; private set; }
        public void SetPressureInPascals(double pressure)
        {
            PressureInPascals = pressure;
        }
        public void SetTemperatureInFahrenheit(double temperature)
        {
            TemperatureInFahrenheit = temperature;
        }

        public override string ToString()
        {
            return $"Давление = {PressureInPascals} Па. Температура = {TemperatureInFahrenheit} °F.";
        }
    }
}
