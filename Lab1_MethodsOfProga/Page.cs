using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class Page
    {
        private long absoluteNumber;
        private byte status;
        public DateTime modTime;
        public byte[] bitMap;
        public int[] values;

        public Page(long absoluteNumber, byte status, DateTime modTime, int[] values, byte[] bitMap)
        {
            AbsoluteNumber = absoluteNumber;
            Status = status;
            this.modTime = modTime;
            this.bitMap = bitMap;
            this.values = values;
        }
        
        public long AbsoluteNumber
        {
            get { return absoluteNumber; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Некорректный ввод абсолютного номера страницы.");
                }
                else
                {
                    absoluteNumber = value;
                }
            }
        }
        public byte Status
        {
            get { return status; }
            set 
            {
                if (value == 0 || value == 1) 
                {
                    status = value;
                } 
                else 
                {
                    throw new ArgumentException("Некорректный ввод статуса изменения страницы.");
                }
            }
        }
    }
}
