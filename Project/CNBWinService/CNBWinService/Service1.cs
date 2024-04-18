using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Drawing;
using MimeKit;
using System.Drawing.Imaging;
using MailKit.Net.Smtp;
using System.Net;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;

namespace CNBWinService
{
    public partial class CNBDown : ServiceBase
    {
        private System.Timers.Timer servicetimer;
        public CNBDown()
        {
            InitializeComponent();
            servicetimer = new Timer(10000);
            servicetimer.Elapsed += Servicetimer_Elapsed;
            Debug.Listeners.Add(new ConsoleWriteListener());
            Debug.Listeners.Add(new FileWriteListener());
            Directory.CreateDirectory(@"..\ServiceHome");

        }
        private void Servicetimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            string filename = @"..\ServiceHome\currencies.txt";
            string xmlfilename = @"..\ServiceHome\currencies.xml";
            string currentdate = DateTime.Now.ToShortDateString();
            string graphfilename = Path.Combine(@"..\ServiceHome", String.Format("currencies_{0}.jpg", currentdate));



            DownloadCurrencies(filename);
            Debug.WriteLine("Currencies downloaded");
            List<Currency> currencies = GetCurrenciesFromFile(filename);
            Debug.WriteLine("Currencies loaded");
            ExportToXML(currencies, xmlfilename);
            Debug.WriteLine("Currencies exported to XML");
            Bitmap im = MakeImage(currencies);
            Debug.WriteLine("Bitmap made");
            im.Save(graphfilename, ImageFormat.Jpeg);
            Debug.WriteLine("Bitmap Saved");
            string sendermail = @"krupadan@email.cz";
            string targetmail = @"daniel.krupa.st@vsb.cz";

            SendAttachment(sendermail, targetmail, graphfilename);
            Debug.WriteLine("Bitmap sent");

        }

        private void DownloadCurrencies(string output_filename)
        {
            try
            {
                using (WebClient wbc = new WebClient())
                {
                    wbc.Encoding = Encoding.UTF8;
                    wbc.DownloadFile(new Uri("http://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt"), output_filename); ;
                }
            }
            catch (DownloadFailedException d)
            {
                Debug.WriteLine(d.Message);
            }

        }

        private List<Currency> GetCurrenciesFromFile(string filename)
        {
            List<Currency> currencies = new List<Currency>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {

                    string date = sr.ReadLine();
                    string header = sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitline = line.Split('|');
                        Currency c = new Currency();
                        c.CountryName = splitline[0];
                        c.Name = splitline[1];
                        c.Price = Convert.ToDouble(splitline[5], CultureInfo.InvariantCulture) / Convert.ToDouble(splitline[3], CultureInfo.InvariantCulture);
                        c.Code = splitline[4];
                        currencies.Add(c);
                        Debug.WriteLine(String.Format("{0}:\t{1}",DateTime.Now.ToString(),line));

                    }
                }
            }
            catch (ReadingFileFailedException r)
            {
                Debug.WriteLine(r.Message);
            }

            return currencies;

        }



        private void SendAttachment(string sender, string target, string attachment_filename)
        {
            try
            {
                MimeMessage mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("Sender", sender));
                mail.To.Add(new MailboxAddress("Target", target));

                BodyBuilder bb = new BodyBuilder();
                bb.Attachments.Add(MimeEntity.Load(attachment_filename));
                bb.TextBody = (String.Format("Sent at {0}", DateTime.Now.ToString()));
                mail.Body = bb.ToMessageBody();

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.seznam.cz", 25, false);
                    smtp.Authenticate("krupadan@email.cz", "iP0nI2fN4oG6");
                    smtp.Send(mail);
                    smtp.Disconnect(true);
                }
            }
            catch (MailFailedException m)
            {
                Debug.WriteLine(m.Message);
            }

        }

        private Bitmap MakeImage(List<Currency> currencies)
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
                        for (int k = i * columnsize; k < columnsize * (i + 1); k++)
                        {
                            b.SetPixel(k, j, currcolor);

                        }
                    });
                    threads[i].Start();
                    Console.WriteLine("Thread " + i);

                }


            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
            return b;
        }

        public void ExportToXML(List<Currency> currencies, string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Currency>));
                using (TextWriter textWriter = new StreamWriter(filename))
                {
                    serializer.Serialize(textWriter, currencies);
                }
                

            }
            catch (XMLExportFailedException x)
            {
                Debug.WriteLine(x.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            servicetimer.Start();
        }

        protected override void OnStop()
        {
            servicetimer.Stop();
        }
    }

    public class Currency
    {
        public string CountryName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
    }

}
