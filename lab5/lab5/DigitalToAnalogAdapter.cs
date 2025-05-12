using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal class DigitalToAnalogAdapter : IAnalogClock
    {
        private readonly DigitalClock _adapter;
        public DigitalToAnalogAdapter(DigitalClock adapter)
        {
            _adapter = adapter;
        }

        public double HourDegrees 
        {
            get
            {
                return _adapter.Hours % 12 * 30 + _adapter.Minutes * 0.5;
            }
            set
            {
                _adapter.Hours = (int)value * 12 / 30;
            }

        }
        public double MinuteDegrees 
        {
            get
            {
                return _adapter.Minutes * 6;
            }
            set
            {
                _adapter.Minutes = (int)value / 6;
            }

        }
        public override string ToString()
        {
            return $"{HourDegrees}:{MinuteDegrees}";
        }

    }
}
