using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal interface IVirtualMemory <T>
    {
        IPage<T> LoadFormFile(long absolutePageNumber);
        long GetPageNumber(long index);
        T GetElementByIndex(long index);
        bool SetElementByIndex(int index, T value);
        void DumpBuffer();
    }
}
