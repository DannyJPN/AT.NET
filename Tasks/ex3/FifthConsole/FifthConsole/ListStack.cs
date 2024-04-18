using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthConsole
{
    public class ListStack
    {
        private List<int> datalist = new List<int>();
        private object threadlocker = new object();
        public void Push(int item)
        {
            lock (threadlocker)
            {
                datalist.Add(item);
            }

        }
        public int Pop()
        {
            int ret;
            lock (threadlocker)
            {
                ret = datalist[datalist.Count - 1];
                datalist.RemoveAt(datalist.Count - 1);
            }
            return ret;
        }

        public bool IsEmpty
        {

            get
            {
                lock (threadlocker)
                {
                    return datalist.Count == 0;
                }
            }

        }

        public bool TryPop(out int ret)
        {
            ret = 0;
            lock (threadlocker)
            {
                if (!IsEmpty)
                {
                    ret = datalist[datalist.Count - 1];
                    datalist.RemoveAt(datalist.Count - 1);
                    return true;
                }

            }
            return false;

        }


    }
}
