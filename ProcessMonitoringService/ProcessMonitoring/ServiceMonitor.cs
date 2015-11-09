using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Windows.Forms;
using System.Configuration;

namespace ProcessMonitoring
{
    public class ServiceMonitor
    {
        List<ProcessMonitor> process = new List<ProcessMonitor>();
        public ServiceMonitor()
        {
            process = ReadingXml();
        }
        
        public void StartProcess()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Start();
            timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInterval"]);
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.StartMonitor); 
        }

        protected void StartMonitor(object sender,ElapsedEventArgs e)
        {
            foreach (ProcessMonitor pm in process)
            {
                if(CheckIfProcessMonitorRequired(pm))
                {
                    if(!IsProcessRunning(pm.ProcessName))
                    {
                        NotifyUser(pm);
                    }
                }
            }           
        }

        private void NotifyUser(ProcessMonitor pm)
        {
            EmailService service = new EmailService();
            service.SendMail(pm);
        }

        private bool IsProcessRunning(string ProcessName)
        {
            Process[] process = Process.GetProcessesByName(ProcessName);
            return (process.Length>0 && process[0].Responding);
        }

        private bool CheckIfProcessMonitorRequired(ProcessMonitor pm)
        {
            bool needMonitoring = false;
            double lastInterval = (DateTime.UtcNow.Subtract(Convert.ToDateTime(pm.LastChecked))).TotalMinutes;
            if (lastInterval > int.Parse(pm.TimeInterval))
            {
                needMonitoring = true;
                pm.LastChecked = DateTime.UtcNow;
            }
            return needMonitoring;
        }

        public List<ProcessMonitor> ReadingXml()
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            List<ProcessMonitor> processes = new List<ProcessMonitor>();
            string path = Path.GetDirectoryName(Application.ExecutablePath)+"\\Process.xml";
            
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("ProcessMonitor");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                ProcessMonitor process = new ProcessMonitor();
                process.ProcessName = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                process.NotificationList = xmlnode[i].ChildNodes.Item(1).InnerText.Trim();
                process.TimeInterval = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                process.NotificationSubject = xmlnode[i].ChildNodes.Item(3).InnerText.Trim();
                process.NotificationMail = xmlnode[i].ChildNodes.Item(4).InnerText.Trim();
                
                processes.Add(process);
            }
            return processes;
        }

        public void GetActiveServices()
        {
            Process[] process = Process.GetProcessesByName("");
        }
    }
}
