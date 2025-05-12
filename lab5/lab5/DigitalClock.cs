using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal class DigitalClock
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public override string ToString()
        {
            return $"{Hours}:{Minutes}";
        }

        public DigitalClock(string time)
        {
            var parts = time.Split(':');
            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int hours) ||
                !int.TryParse(parts[1], out int minutes))
            {
                throw new ArgumentException("Invalid time format. Use 'hh.mm'");
            }
            Hours = hours;
            Minutes = minutes;
        }

    }
}
