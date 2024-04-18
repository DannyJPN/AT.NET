using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthConsoleAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {

           await  WriterAsync();


        }



        private static async Task WriterAsync()
        {

            StreamWriter w = new StreamWriter("Asyncfile.txt");

            for (int i = 0; i < DateTime.Now.Millisecond; i++)
            {
                await w.WriteLineAsync(String.Format("Line No.{0}", i));

            }

            w.Close();

        }
    }
}
