using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLConsole
{
    class News
    {
        public string title;
        public string link;
        public override string ToString()
        {
            return String.Format("{0} \t\t\t\t\t {1}", title, link);
        }
    }
}
