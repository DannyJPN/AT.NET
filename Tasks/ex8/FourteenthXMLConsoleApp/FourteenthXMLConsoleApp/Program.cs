using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FourteenthXMLConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "Finances.txt";
            WebClient wbc = new WebClient();
            wbc.Encoding = Encoding.UTF8;
            wbc.DownloadFile(new Uri("http://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt"),filename ); ;
            wbc.Dispose();

            List<string> currencies = new List<string>();
            StreamReader sr = new StreamReader(filename);
            double eurcur = 0;
            XmlDocument xmldoc = new XmlDocument();
            XmlNode root = xmldoc.CreateElement("currencies");
            xmldoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xmldoc.AppendChild(root);
            while (!sr.EndOfStream)
            {
                
                string line = sr.ReadLine();
                if (line.Contains(" ")) { continue; }
                    string[] splitline = line.Split('|');
                
                currencies.Add(line);
                Console.WriteLine(line);
                XmlNode countrynode = xmldoc.CreateElement("country");
                root.AppendChild(countrynode);
                XmlAttribute xmlattr = xmldoc.CreateAttribute("name");
                xmlattr.Value = splitline[0];
                countrynode.Attributes.Append(xmlattr);
                countrynode.AppendChild(xmldoc.CreateElement("name").AppendChild(xmldoc.CreateTextNode(splitline[1])));
                countrynode.AppendChild(xmldoc.CreateElement("amount").AppendChild(xmldoc.CreateTextNode(splitline[2])));
                countrynode.AppendChild(xmldoc.CreateElement("code").AppendChild(xmldoc.CreateTextNode(splitline[3])));
                countrynode.AppendChild(xmldoc.CreateElement("price").AppendChild(xmldoc.CreateTextNode(splitline[4])));

                if (splitline[3] == "EUR")
                {
                    eurcur = Convert.ToDouble(splitline[4], CultureInfo.InvariantCulture);
                }
            }
            Console.WriteLine("\n\nEUR:\t{0}",eurcur);




            sr.Close();
            
          
          

            



            xmldoc.Save("currencies.xml");
            




        }
    }
}
