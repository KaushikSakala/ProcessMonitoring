using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProcessMonitoring
{
    public class EmailService
    {
        public void SendMail(ProcessMonitor ProcessMonitor)
        {
            // Gmail Address from where you send the mail
            string fromAddress = ConfigurationManager.AppSettings["UserName"];
            
            // any address where the email will be sending
            List<string> toAddress = GetToAdrress(ProcessMonitor.NotificationList);
            
            //Password of your mail address
            const string fromPassword = "";
            
            // Passing the values and make a email formate to display
            string subject = ProcessMonitor.NotificationSubject;
          
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                string ServerName = ConfigurationManager.AppSettings["EmailHost"].ToString();
                
                smtp.Host = ServerName;
                smtp.UseDefaultCredentials = true;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
            }

            MailMessage mail = new MailMessage();
            mail.Subject = subject;
            mail.From = new MailAddress(fromAddress);
            mail.Priority = MailPriority.High;
            mail.Body = subject;
            foreach (string address in toAddress)
            {
                mail.To.Add(address);                
            }
            // Passing mail object to smtp object
            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                LogException logFile = new LogException();
                logFile.Logging(e.Message+"\n\n"+e.StackTrace);
            }
        }

        private List<string> GetToAdrress(string mailList)
        {
            List<string> mailAddress = new List<string>();
            if(mailList.Contains(';'))
            {
                mailAddress = mailList.Split(';').ToList();
            }
            else
            {
                mailAddress.Add(mailList);
            }
            return mailAddress;
        }
    }
}
