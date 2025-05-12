using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal interface IAnalogClock
    {
        public double HourDegrees { get; set; }
        public double MinuteDegrees { get; set; }
    }
}
