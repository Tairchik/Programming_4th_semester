namespace TempConverter
{
    public interface ITemperatureConverter
    {
        double ConvertToCelsius(double temperature);
        double ConvertFromCelsius(double celsius);
    }
}
