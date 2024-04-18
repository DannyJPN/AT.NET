using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeventhWebApplication.Services
{
    public class ExceptionHandler
    {
        private Ifaces.ILogger logger;
        public ExceptionHandler(Ifaces.ILogger logger)
        { this.logger = logger; }


        public void Handle(Exception ex)
        {
            Console.WriteLine(String.Format("Message: {0}\n",ex.Message));
            logger.Log(ex);
        }
    }
}
