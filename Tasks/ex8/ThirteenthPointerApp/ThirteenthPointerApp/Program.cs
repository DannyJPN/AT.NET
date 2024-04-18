using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirteenthPointerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Genstring();
            Console.WriteLine(text);
            Stopwatch stpw = new Stopwatch();
                stpw.Start();
            int suma = FindLetterFunctions(text, 'c');
            stpw.Stop();
            Console.WriteLine("Function made {1} matches it in {0} millis", stpw.ElapsedMilliseconds,suma);
            stpw.Reset();

            stpw.Start();
            suma = FindLetterPointers(text,text.Length, 'c');
            stpw.Stop();
            Console.WriteLine("Pointer made {1} matches it in {0} millis", stpw.ElapsedMilliseconds,suma);




            stpw.Reset();

            stpw.Start();
            for (int i = 0; i < text.Length; i++)
            {
                for(int j = i+1; j < text.Length; j++)
                { int sum = FindLetterFunctions(text.Substring(i,j-i), 'c'); }
                
                

            }
            stpw.Stop();
            Console.WriteLine("Function made it in {0} millis", stpw.ElapsedMilliseconds, suma);
            stpw.Reset();

            stpw.Start();
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = i+1; j < text.Length; j++)
                { int sum = FindLetterPointers(text,j-i, 'c'); }



            }
            stpw.Stop();
            Console.WriteLine("Pointers made it in {0} millis", stpw.ElapsedMilliseconds, suma);


            //---------------------------------------------------














        }



        public static unsafe int FindLetterPointers(string text, int len, char searched)
        {
            int count = 0;
            fixed (char* charpointer = text)
            {
                for (int i = 0; i < len; i++)
                {
                    if (charpointer[i] == searched)
                    { count++; }
                }
            }


            return count;


        }
        public static  int FindLetterFunctions(string text,char searched)
        {
            int count = 0;
            
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == searched)
                    { count++; }
                }
            


            return count;

        }


        public static string Genstring()
        {
            string text = "";
            Random n = new Random();
            for (int i = 0; i < n.Next(1500000, 50000000); i++)
            {
                text += Convert.ToChar(n.Next(Convert.ToInt32('0'), Convert.ToInt32('z')+1)).ToString();

            }
            return text;
        }
    }
}
