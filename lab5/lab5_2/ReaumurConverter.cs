using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_2
{
    internal class ReaumurConverter
    {
        public double ReaumurToCelsius(double reaumur)
        {
            return reaumur * 1.25;
        }

        public double CelsiusToReaumur(double celsius)
        {
            return celsius / 1.25;
        }
    }
}
