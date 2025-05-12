using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace lab5_2
{
    internal class ReaumurConverter
    {
        public double ReaumurToCelsius(double reaumur)
        {
            return Temperature.FromDegreesReaumur(reaumur).DegreesCelsius;
        }

        public double CelsiusToReaumur(double celsius)
        {
            return Temperature.FromDegreesCelsius(celsius).DegreesReaumur;
        }
    }
}
