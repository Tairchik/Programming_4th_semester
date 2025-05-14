using TempConverter;

namespace lab5_2
{
    internal class ReaumurToCelsiusAdapter : ITemperatureConverter
    {
        private readonly ReaumurConverter _reaumurConverter;

        public ReaumurToCelsiusAdapter(ReaumurConverter reaumurConverter)
        {
            _reaumurConverter = reaumurConverter;
        }

        public double ConvertToCelsius(double reaumur)
        {
            // Используем существующий метод для перевода из Реомюра в Цельсия
            return _reaumurConverter.ReaumurToCelsius(reaumur);
        }

        public double ConvertFromCelsius(double celsius)
        {
            // Используем существующий метод для перевода из Цельсия в Реомюра
            return _reaumurConverter.CelsiusToReaumur(celsius);
        }
    }
}
