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
            IVirtualMemory<string> a = new VirtualMemoryChar("Test3", 100005, 20);

            
            Console.WriteLine(a.GetElementByIndex(100000));
       
            a.DumpBuffer();
        }
    }
}
