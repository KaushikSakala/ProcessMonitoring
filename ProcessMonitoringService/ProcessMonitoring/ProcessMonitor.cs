using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitoring
{
    public class ProcessMonitor
    {
        public int ID { get; set; }
        public string ProcessName { get; set; }
        public string NotificationList { get; set; }
        public string TimeInterval { get; set; }
        public DateTime LastChecked { get; set; }
        public string NotificationSubject { get; set; }
        public string NotificationMail { get; set; }
    }
}
