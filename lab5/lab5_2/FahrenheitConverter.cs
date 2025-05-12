using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_2
{
    internal class FahrenheitConverter : ITemperatureConverter
    {
        public double ConvertToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        public double ConvertFromCelsius(double celsius)
        {
            return celsius * 9 / 5 + 32;
        }
    }
}
