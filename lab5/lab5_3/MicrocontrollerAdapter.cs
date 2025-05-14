
    namespace lab5_3
    {
        // Адаптер для микроконтроллеров, работающих с Паскалями и Фаренгейтами
        public class MicrocontrollerAdapter : IProcessController
        {
            private readonly MicroController _microcontroller;

            public MicrocontrollerAdapter(MicroController microcontroller)
            {
                _microcontroller = microcontroller;
            }

            public double PressureInAtm
            {
                get
                {
                    return _microcontroller.PressureInPascals / 101325;
                }
                set
                {
                    _microcontroller.SetPressureInPascals(value * 101235);
                }
            }

            public double TemperatureInCelsius
            {
                get
                {
                    return (_microcontroller.TemperatureInFahrenheit - 32) * 5 / 9;
                }
                set
                {
                    _microcontroller.SetTemperatureInFahrenheit((value * 9 / 5) + 32);
                }
            }

            public override string ToString()
            {
                return $"Давление = {PressureInAtm} Атм. Температура = {TemperatureInCelsius} °C.";
            }

        }

    }
