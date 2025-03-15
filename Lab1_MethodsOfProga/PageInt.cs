using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class PageInt : IPage<int>
    {
        private long absoluteNumber;
        private byte status;
        private DateTime modTime;
        private byte[] bitMap;
        private int[] values;
        public PageInt(long absoluteNumber, byte status, DateTime modTime, int[] values, byte[] bitMap)
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

        public DateTime ModTime
        {
            get { return modTime; }
            set { modTime = value; }
        }
        public byte[] BitMap
        {
            get { return bitMap; }
            set { bitMap = value; }
        }
        public int[] Values
        {
            get { return values; }
            set { values = value; }
        }
    }
}
