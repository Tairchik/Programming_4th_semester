using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal interface IPage<T>
    {        
        long AbsoluteNumber { get; set; }
        byte Status {  get; set; }
        DateTime ModTime { get; set; }
        byte[] BitMap { get; set; }
        T[] Values { get; set; }
    }
}
