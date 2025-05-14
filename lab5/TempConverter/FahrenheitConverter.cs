namespace TempConverter
{

    public class FahrenheitConverter : ITemperatureConverter
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