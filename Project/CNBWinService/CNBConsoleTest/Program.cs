using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MimeKit;
using System.Drawing.Imaging;
using MailKit.Net.Smtp;
using System.Net;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Threading;

namespace CNBConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"..\ServiceHome\currencies.txt";
            string xmlfilename = @"..\ServiceHome\currencies.xml";
            string currentdate = DateTime.Now.ToShortDateString();
            string graphfilename = Path.Combine(@"..\ServiceHome", String.Format("currencies_{0}.jpg", currentdate));


            DownloadCurrencies(filename);
            List<Currency> currencies = GetCurrenciesFromFile(filename);
            ExportToXML(currencies, xmlfilename);
            Bitmap im = MakeImage(currencies);
            im.Save(graphfilename, ImageFormat.Jpeg);
            string sendermail = @"krupadan@email.cz";
            string targetmail = @"daniel.krupa.st@vsb.cz";

            SendAttachment(sendermail, targetmail, graphfilename);
            List<Currency> loadedcur = LoadFromXML(xmlfilename);
           
             foreach (Currency c in loadedcur)
             {
                 Console.WriteLine(c);
             }
            Console.WriteLine(loadedcur.Count);


            List<LeisureObjectDTO> companies = new List<LeisureObjectDTO>();
            companies.Add(new InsuranceCompanyDTO(1, 111, "VZP", "Všeobecná zdravotní pojišťovna"));
            companies.Add(new InsuranceCompanyDTO(2, 201, "VZPČR", "Vojenská zdravotní pojišťovna České republiky"));
            companies.Add(new InsuranceCompanyDTO(3, 205, "ČPZP", "Česká průmyslová zdravotní pojišťovna"));
            companies.Add(new InsuranceCompanyDTO(4, 207, "OZPZBPS", "Oborová zdravotní pojišťovna zaměstnanců bank pojišťoven a stavebnictví"));
            companies.Add(new InsuranceCompanyDTO(5, 209, "ZPŠ", "Zaměstnanecká pojišťovna Škoda"));
            companies.Add(new InsuranceCompanyDTO(6, 211, "ZPMVČR", "Zdravotní pojišťovna ministerstva vnitra České republiky"));
            companies.Add(new InsuranceCompanyDTO(7, 213, "RBPZP", "Revírní bratrská pokladna - zdravotní pojišťovna"));
          //  ExportToXML(companies, "InsuranceCompanyDTO.xml",true);

        }


        private static void DownloadCurrencies(string output_filename)
        {

            WebClient wbc = new WebClient();
            wbc.Encoding = Encoding.UTF8;
            wbc.DownloadFile(new Uri("http://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt"), output_filename); ;
            wbc.Dispose();


        }

        private static List<Currency> GetCurrenciesFromFile(string filename)
        {
            List<Currency> currencies = new List<Currency>();
            StreamReader sr = new StreamReader(filename);

            string date = sr.ReadLine();
            string header = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] splitline = line.Split('|');
                Currency c = new Currency();
                c.CountryName = splitline[0];
                c.Name = splitline[1];
                c.Price = Convert.ToDouble(splitline[4], CultureInfo.InvariantCulture) / Convert.ToDouble(splitline[2], CultureInfo.InvariantCulture);
                c.Code = splitline[3];
                currencies.Add(c);


            }
            sr.Close();
            return currencies;
        }



        private static void SendAttachment(string sender, string target, string attachment_filename)
        {
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Sender", sender));
            mail.To.Add(new MailboxAddress("Target", target));

            BodyBuilder bb = new BodyBuilder();
            bb.Attachments.Add(attachment_filename);
            bb.TextBody = (String.Format("Sent at {0}", DateTime.Now.ToString()));
            mail.Body = bb.ToMessageBody();

            SmtpClient smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;


            smtp.Connect("smtp.seznam.cz", 465, true);
            smtp.Authenticate("krupadan@email.cz", "iP0nI2fN4oG6");
            smtp.Send(mail);
            smtp.Disconnect(true);


        }

        private static Bitmap MakeImage(List<Currency> currencies)
        {
            Bitmap b = new Bitmap(1800, 1000);
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    b.SetPixel(i, j, Color.White);
                }

            }
            int columnsize = b.Width / currencies.Count;
            Thread[] threads = new Thread[currencies.Count];
     

            for (int i = 0; i < currencies.Count; i++)
            {
                
                string code = currencies[i].Code.ToLower();
                Color currcolor = Color.FromArgb(Convert.ToInt32(code[0]), Convert.ToInt32(code[1]), Convert.ToInt32(code[2]));
                for (int j = 0; j < currencies[i].Price * 10; j++)
                {
                    threads[i] = new Thread(() => { 
                    for (int k = i*columnsize; k < columnsize*(i+1); k++)
                    {
                        b.SetPixel(k, j, currcolor);

                    }
                    });
                    threads[i].Start();
                    Console.WriteLine("Thread "+i);

                }


            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
            return b;
        }

        static public void ExportToXML(List<Currency> currencies, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Currency>));
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, currencies);
            textWriter.Close();
        }
        static public List<Currency> LoadFromXML( string filename)
        {
            List<Currency> currencies;
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Currency>));
            TextReader textReader = new StreamReader(filename);
          
            currencies = (List<Currency>)deserializer.Deserialize(textReader);
            textReader.Close();

            return currencies;
        }
        static public void ExportToXML(List<LeisureObjectDTO> currencies, string filename,bool nic=true)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LeisureObjectDTO>));
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, currencies);
            textWriter.Close();
        }
        static public List<LeisureObjectDTO> LoadFromXML(string filename,bool s=true)
        {
            List<LeisureObjectDTO> currencies;
            XmlSerializer deserializer = new XmlSerializer(typeof(List<LeisureObjectDTO>));
            TextReader textReader = new StreamReader(filename);

            currencies = (List<LeisureObjectDTO>)deserializer.Deserialize(textReader);
            textReader.Close();

            return currencies;
        }


    }


    public class Currency
    {
        public string CountryName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public override string ToString()
        {
            return String.Format("Name {0}\tCountryName {1}\tPrice {2}\tCode {3}",Name,CountryName,Price,Code);
        }
    }

    [XmlRoot]
    public class InsuranceCompanyDTO : LeisureObjectDTO
    {
        public InsuranceCompanyDTO() : base()
        { }
  
     

        public InsuranceCompanyDTO(int iD, int v1, string v2, string v3) : base(iD)
        {
            this.Number = v1;
            this.Abbreviation = v2;
            this.FullName = v3;
        }

        public int Number { get; set; }
         public string Abbreviation { get; set; }
         public string FullName { get; set; }

    }

    public class LeisureObjectDTO
    {
    public LeisureObjectDTO() { }
        public int ID { get; set; }
        public LeisureObjectDTO(int ID)
        { this.ID = ID; }
    }
}
