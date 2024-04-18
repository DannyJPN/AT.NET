using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XMLConsole
{
    class Downloader
    {
        public void DownloadXML(Uri uri,string filename)
        {
            WebClient wb = new WebClient();
            wb.DownloadFile(uri, filename);
            wb.Dispose();


        }
    }
}
