using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SeventhWebApplication.Ifaces;

namespace SeventhWebApplication.Services
{
    public class TxtLogger: Ifaces.ILogger
    {
        private IHostingEnvironment env;
        public TxtLogger(IHostingEnvironment ihenv)
        { env = ihenv; }

    
        public void Log(Exception ex)
        {

            string logpath = Path.Combine(env.ContentRootPath, "log.txt");
            File.AppendAllText(logpath,String.Format("{0}{1}\t{2}\n",DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), ex.Message));
        }

    
    }
}
