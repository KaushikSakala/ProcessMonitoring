using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
namespace ProcessMonitoring
{
    public class LogException
    {
        public void Logging(string exception)
        {
            string logFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\LogFile_" + DateTime.UtcNow.ToString("MMddyyyy") + ".txt";
            if (ConfigurationManager.AppSettings["LogFile"].ToLower() == "y")
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(exception);
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
}
