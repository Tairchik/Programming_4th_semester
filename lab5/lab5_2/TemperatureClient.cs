using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_2
{
    internal class TemperatureClient
    {
        private ITemperatureConverter _converter;

        public TemperatureClient(ITemperatureConverter converter)
        {
            _converter = converter;
        }

        public void SetConverter(ITemperatureConverter converter)
        {
            _converter = converter;
        }

        public double GetCelsius(double temperature)
        {
            return _converter.ConvertToCelsius(temperature);
        }

        public double GetOriginalTemperature(double celsius)
        {
            return _converter.ConvertFromCelsius(celsius);
        }
    }
}
