using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthConsole
{
    class Program
    {
        static void Main(string[] args)

        {
            FileSystemWatcher fsw = new FileSystemWatcher(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DirToWatch"));

            Console.WriteLine("Watching {0}",AppDomain.CurrentDomain.BaseDirectory);
            fsw.IncludeSubdirectories = true;
            fsw.Changed += Fsw_Changed;
            fsw.Created += Fsw_Created;
            fsw.Deleted += Fsw_Deleted;
            fsw.Disposed += Fsw_Disposed;
            fsw.Error += Fsw_Error;
            fsw.Renamed += Fsw_Renamed;

            fsw.EnableRaisingEvents = true;
            while (true)
            {
                if (Console.ReadLine() == "log")
                {
                    IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
                    List<string> recs = new List<string>();
                    StreamReader sr = new StreamReader(isol.OpenFile("Application.log", FileMode.Open));
                    
                    string record;
                    while ((record = sr.ReadLine()) != null)
                    {
                        recs.Add(record);
                    }

                    for (int i = recs.Count - 6; i < recs.Count; i++)
                    { Console.WriteLine(recs[i]); }


                    sr.Close(); isol.Close();

                }
                else if (Console.ReadLine() == "exit")
                {
                    fsw.Dispose();
                    return;
                }
                else if (Console.ReadLine() == "mail")
                {
                    MimeMessage mes = new MimeMessage();
                    mes.To.Add(new MailboxAddress("krupadan@email.cz"));
                    mes.From.Add(new MailboxAddress("atnet2019@seznam.cz"));
                    mes.Subject = "Logfile";
                    BodyBuilder b = new BodyBuilder();b.TextBody = String.Format("Logfile at {0}", DateTime.Now.ToString());
                    IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
                    StreamReader sr = new StreamReader(isol.OpenFile("Application.log", FileMode.Open));
                    b.Attachments.Add("Application.log",sr.BaseStream);

                    mes.Body = b.ToMessageBody();
                    sr.Close(); isol.Close();

                    SmtpClient smtp = new SmtpClient();




                    smtp.Connect("smtp.seznam.cz", 465, true);
                    smtp.Authenticate("atnet2019@seznam.cz", "janousek");
                    smtp.Send(mes);
                    smtp.Disconnect(true);
                    smtp.Dispose();

                }



            }

            
        }

        private static void Fsw_Renamed(object sender, RenamedEventArgs e)
        {

            IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();

                StreamWriter sw = new StreamWriter(isol.OpenFile( "Application.log",FileMode.Append));
            sw.WriteLine("{0}\tFile {1} was renamed to {2}", DateTime.Now.ToString(),e.OldName,e.Name);
            sw.Close();isol.Close();
            Console.WriteLine("{0}\tFile {1} was renamed to {2}", DateTime.Now.ToString(), e.OldName, e.Name);
            
        }

        private static void Fsw_Error(object sender, ErrorEventArgs e)
        {
            IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
            StreamWriter sw = new StreamWriter(isol.OpenFile( "Application.log",FileMode.Append));
            sw.WriteLine("{0}\tError type {1}", DateTime.Now.ToString(),e.GetException().Message);
            sw.Close();isol.Close();
            Console.WriteLine("{0}\tError type {1}", DateTime.Now.ToString(),e.GetException().Message);
        }

        private static void Fsw_Disposed(object sender, EventArgs e)
        {
            IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
            StreamWriter sw = new StreamWriter(isol.OpenFile("Application.log", FileMode.Append));
            sw.WriteLine("{0}\tFile {1} was disposed", DateTime.Now.ToString(), e.ToString());
            sw.Close();isol.Close();
            Console.WriteLine("{0}\tFile {1} was disposed", DateTime.Now.ToString(), e.ToString());
        }

        private static void Fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
            StreamWriter sw = new StreamWriter(isol.OpenFile("Application.log", FileMode.Append));
            sw.WriteLine("{0}\tFile {1} was deleted", DateTime.Now.ToString(), e.Name);
            sw.Close();isol.Close();
            Console.WriteLine("{0}\tFile {1} was deleted", DateTime.Now.ToString(), e.Name);
        }

        private static void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
            StreamWriter sw = new StreamWriter(isol.OpenFile("Application.log", FileMode.Append));
            sw.WriteLine("{0}\tFile {1} was created", DateTime.Now.ToString(), e.Name);
            sw.Close();isol.Close();
            Console.WriteLine("{0}\tFile {1} was created", DateTime.Now.ToString(), e.Name);
        }

        private static void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            IsolatedStorageFile isol = IsolatedStorageFile.GetUserStoreForAssembly();
            StreamWriter sw = new StreamWriter(isol.OpenFile("Application.log", FileMode.Append));
            sw.WriteLine("{0}\tFile {1} was changed", DateTime.Now.ToString(), e.Name);
            sw.Close();isol.Close();
            Console.WriteLine("{0}\tFile {1} was changed", DateTime.Now.ToString(), e.Name);
        }
    }
}
