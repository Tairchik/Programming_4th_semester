using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            VirtualMemoryInteger a = new VirtualMemoryInteger("Test1", 100001);

            Console.WriteLine(a.GetPageNumber(1233));
        }
    }
}
