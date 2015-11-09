using ProcessMonitoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessMonitoringService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ServiceMonitor read = new ServiceMonitor();
            read.ReadingXml();

        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            ServiceMonitor service = new ServiceMonitor();
            service.StartProcess();
        }
    }
}
