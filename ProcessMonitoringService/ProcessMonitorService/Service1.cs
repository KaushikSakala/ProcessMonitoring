using ProcessMonitoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitorService
{
    public partial class Service1 : ServiceBase
    {
        ServiceMonitor service;
        public Service1()
        {
            InitializeComponent();
            service = new ServiceMonitor();
        }

        protected override void OnStart(string[] args)
        {            
            service.StartProcess();
        }

        protected override void OnStop()
        {
            service = null;
        }
    }
}
