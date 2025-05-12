using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace lab5
{
    internal class Client
    {
        private IAnalogClock _clock;

        public Client(IAnalogClock clock)
        {
            _clock = clock;
        }

        public void SetConverter(IAnalogClock clock)
        {
            _clock = clock;
        }

        public double GetAnalogHours()
        {
            return _clock.HourDegrees;
        }
        public double GetAnalogMinute()
        {
            return _clock.MinuteDegrees;
        }
        public override string ToString()
        {
            return _clock.ToString();
        }
    }
}
