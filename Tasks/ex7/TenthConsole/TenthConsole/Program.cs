using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenthConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            Debugger.Launch();


            double a = 10;
            double b = 5;

            Console.WriteLine("{0} + {1} = {2}", a, b, a + b);
            Console.WriteLine("{0} - {1} = {2}", a, b, a - b);
            Console.WriteLine("{0} * {1} = {2}", a, b, a * b);
            Console.WriteLine("{0} / {1} = {2}", a, b, a / b);
            Debugger.Break();

            Debug.Listeners.Add(new FileWriteListener());
            Debug.WriteLine("Debugging is on");
            
            Debug.WriteLine("Indented Debugging is on");
            Trace.Listeners.Add(new FileWriteListener());
            Trace.WriteLine("Tracing is on");
            Trace.WriteLine("Indented Tracing is on");
            

        }




    }




}
