using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_2_2
{
    internal interface ITemperatureConverter
    {
        double ConvertToCelsius(double temperature);
        double ConvertFromCelsius(double celsius);
    }
}
