using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "rss.xml";
            Downloader down = new Downloader();
            down.DownloadXML(new Uri("https://news.ycombinator.com/rss"), filename);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            XmlNode root = xmldoc.DocumentElement;

        
            XmlNode main = root.FirstChild;
            XmlNodeList xmllisttitles = xmldoc.GetElementsByTagName("title");
            XmlNodeList xmllistlinks = xmldoc.GetElementsByTagName("link");



            List<News> newlist = new List<News>();
            for (int i = 0; i < xmllisttitles.Count; i++)
            {
                newlist.Add(new News() { title = xmllisttitles[i].InnerText });
               // Console.WriteLine(xmllisttitles[i].InnerText);
            }
            for (int i = 0; i < xmllistlinks.Count; i++)
            {
                newlist[i].link = xmllistlinks[i].InnerText ;
               // Console.WriteLine(xmllistlinks[i].InnerText);
            }

            for (int i = 0; i < newlist.Count; i++)
            {
                Console.WriteLine(newlist[i].ToString());

            }

        }
    }
}
