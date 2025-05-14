using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempConverter;

namespace lab5_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITemperatureConverter fahrenheitConverter = new FahrenheitConverter();
            ReaumurConverter reaumurConverter = new ReaumurConverter();

            ITemperatureConverter reaumurAdapter = new ReaumurToCelsiusAdapter(reaumurConverter);

            TemperatureClient client = new TemperatureClient(fahrenheitConverter);

            double fahrenheitTemperature;
            Console.Write("Введите температуру в фаренгейтах: ");
            fahrenheitTemperature = double.Parse(Console.ReadLine());
            Console.WriteLine($"{fahrenheitTemperature}°F = {client.GetCelsius(fahrenheitTemperature):F2}°C");

            client.SetConverter(reaumurAdapter);

            double reaumurTemperature;
            Console.Write("Введите температуру в размерности ремеамура: ");
            reaumurTemperature = double.Parse(Console.ReadLine());
            Console.WriteLine($"{reaumurTemperature}°R = {client.GetCelsius(reaumurTemperature):F2}°C");

            Console.Write("Введите температуру в размерности ремеамура: ");
            double celsiusTemperature = double.Parse(Console.ReadLine());
            Console.WriteLine($"{celsiusTemperature}°C = {client.GetOriginalTemperature(celsiusTemperature):F2}°R");
        }
    }
}
