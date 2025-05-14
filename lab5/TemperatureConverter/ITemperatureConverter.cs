using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter
{
    internal interface ITemperatureConverter
    {
        double ConvertToCelsius(double temperature);
        double ConvertFromCelsius(double celsius);
    }
}
