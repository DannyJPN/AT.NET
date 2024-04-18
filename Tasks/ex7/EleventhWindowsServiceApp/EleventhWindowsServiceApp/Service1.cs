using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EleventhWindowsServiceApp
{
    public partial class Service1 : ServiceBase
    {

        private Timer servicetimer;
        public Service1()
        {
            InitializeComponent();
            servicetimer = new Timer(10000);
            servicetimer.Elapsed += Servicetimer_Elapsed;

        }

        private void Servicetimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(Path.Combine(Path.GetTempPath(),"ServiceLog.log"));
            sw.WriteLine(DateTime.Now.ToString());
            sw.Close();
           
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
}
