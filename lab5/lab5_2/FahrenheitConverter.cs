using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace lab5_2
{
    internal class FahrenheitConverter : ITemperatureConverter
    {
        public double ConvertToCelsius(double fahrenheit)
        {
            return Temperature.FromDegreesFahrenheit(fahrenheit).DegreesCelsius;
        }

        public double ConvertFromCelsius(double celsius)
        {
            return Temperature.FromDegreesCelsius(celsius).DegreesFahrenheit;
        }
    }
}
