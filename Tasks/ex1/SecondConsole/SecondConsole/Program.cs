using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"Current name: {AppDomain.CurrentDomain.FriendlyName}");
            Console.WriteLine($"Current name: {AppDomain.CurrentDomain.BaseDirectory}");
            for (int i = 0; i < args.Length; i++)
            {
                Console.Write(args[i].ToUpper());

            }
            Console.WriteLine("\n_____________________________");
          
            File.WriteAllText("data.txt", string.Join("\n", args.Select(x => x.ToUpper())));

        }
    }
}
