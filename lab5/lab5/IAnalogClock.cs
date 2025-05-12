using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal interface IAnalogClock
    {
        public int HourDegrees { get; set; }
        public int MinuteDegrees { get; set; }
    }
}
