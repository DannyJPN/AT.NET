using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FifthConsole
{
    class Program
    {
        private static ListStack l = new ListStack();
        private static object threadlocker = new object();
        private static void StThr(Action<int> fn, int data)
        {
            var t = new Thread(()=> { fn(data); });
        }





        static void Main(string[] args)
        {
            Thread singlepusher = new Thread(ThreadRunInsert);
            singlepusher.Name = "Singler";
            Thread[] fivegroup = new Thread[5];
            singlepusher.Start();
           
            for (int i = 0; i < fivegroup.Length; i++)
            {
                fivegroup[i] = new Thread(ThreadRunRemove);
                fivegroup[i].Name = i.ToString();
                fivegroup[i].Start();

            }

            singlepusher.Join();
            for (int i = 0; i < fivegroup.Length; i++)
            {
                fivegroup[i].Join();

            }

        }

        private static void ThreadRunInsert()
        {
            Random n = new Random(DateTime.Now.Millisecond);

            while (DateTime.Now.Hour != 0)
            {
                
                    int x = n.Next();
                lock (threadlocker)
                {
                    l.Push(x);
                    //Console.WriteLine("{0} Pushed",x);
                    Thread.Sleep(100);
                    Monitor.Pulse(threadlocker);
                }

        

            }

        }


        private static void ThreadRunRemove()
        {
            Random n = new Random(DateTime.Now.Millisecond);

            while (DateTime.Now.Hour != 0)
            {
                
                for (int i = 0; i < n.Next(DateTime.Now.Millisecond % 50); i++)
                {

                    lock (threadlocker)
                    {
                        if (l.TryPop(out int r))
                        {
                            Console.WriteLine("Thread {0} popped value {1}.", Thread.CurrentThread.Name, r);
                        }
                        else
                        {
                            Console.WriteLine("Thread {0} attempted to pop a empty stack.", Thread.CurrentThread.Name);
                            Monitor.Wait(threadlocker);
                        }
                    }

                }

                //Thread.Sleep(n.Next(40, 1001));
            }

        }

        private static void ThreadRunRandomAction()
        {
            Random n = new Random(DateTime.Now.Millisecond);

            while (DateTime.Now.Hour != 0)
            {
                for (int i = 0; i < n.Next(DateTime.Now.Millisecond % 50); i++)
                {
                    int x = n.Next();
                    l.Push(x);
                    //Console.WriteLine("{0} Pushed",x);
                }
                for (int i = 0; i < n.Next(DateTime.Now.Millisecond % 50); i++)
                {


                    l.TryPop(out int r);
                    // Console.WriteLine("{0} Popped", l.Pop());
                    //Console.WriteLine("Attempt to pop a empty stack");

                }


            }

        }

    }
}
