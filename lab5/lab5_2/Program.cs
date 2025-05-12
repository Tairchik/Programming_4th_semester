using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_2_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITemperatureConverter fahrenheitConverter = new FahrenheitConverter();
            ReaumurConverter reaumurConverter = new ReaumurConverter();

            ITemperatureConverter reaumurAdapter = new ReaumurToCelsiusAdapter(reaumurConverter);

            TemperatureClient client = new TemperatureClient(fahrenheitConverter);

            double fahrenheitTemperature = 68;
            Console.WriteLine($"{fahrenheitTemperature}°F = {client.GetCelsius(fahrenheitTemperature):F2}°C");

            client.SetConverter(reaumurAdapter);

            double reaumurTemperature = 20;
            Console.WriteLine($"{reaumurTemperature}°R = {client.GetCelsius(reaumurTemperature):F2}°C");

            // Обратная конвертация из Цельсия в Реомюра
            double celsiusTemperature = 25;
            Console.WriteLine($"{celsiusTemperature}°C = {client.GetOriginalTemperature(celsiusTemperature):F2}°R");

        }
    }
}
