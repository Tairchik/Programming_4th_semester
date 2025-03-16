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
            VirtualMemoryString a = new VirtualMemoryString("Test4", 100005, 50);
            Console.WriteLine(a.SetElementByIndex(0, "dIMA"));
            a.DumpBuffer(); 
        }
    }
}
