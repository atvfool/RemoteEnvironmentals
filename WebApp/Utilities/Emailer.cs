using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Emailer
    {
        private string SMTPHost { get; set; }
        private string SMTPUsername { get; set; }
        private string SMTPPassword { get; set; }
        private string FromEmail { get; set; }
        private string ToEmail { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }

        private const string SMTP_HOST_SETTING = "SMTP_HOST";
        private const string SMTP_USERNAME_SETTING = "SMTP_USERNAME";
        private const string SMTP_PASSWORD_SETTING = "SMTP_PASSWORD";
        private const string FROM_EMAIL_SETTING = "FROM_EMAIL";
        private const string TO_EMAIL_SETTING = "TO_EMAIL";

        public Emailer() {
            string temp = null;
            bool isValid = true;
            
            temp = Environment.GetEnvironmentVariable(SMTP_HOST_SETTING);
            if (temp == null)
            {
                Console.WriteLine("You must set your '" + SMTP_HOST_SETTING + "' environment variable.");
                isValid = false;
            }
            else
            {
                SMTPHost = temp;
            }
            
            temp = Environment.GetEnvironmentVariable(SMTP_USERNAME_SETTING);
            if (temp == null)
            {
                Console.WriteLine("You must set your '" + SMTP_USERNAME_SETTING + "' environment variable.");
                isValid = false;
            }
            else
            {
                SMTPUsername = temp;
            }

            temp = Environment.GetEnvironmentVariable(SMTP_PASSWORD_SETTING);
            if (temp == null)
            {
                Console.WriteLine("You must set your '" + SMTP_PASSWORD_SETTING + "' environment variable.");
                isValid = false;
            }
            else
            {
                SMTPPassword = temp;
            }

            temp = Environment.GetEnvironmentVariable(FROM_EMAIL_SETTING);
            if (temp == null)
            {
                Console.WriteLine("You must set your '" + FROM_EMAIL_SETTING + "' environment variable.");
                isValid = false;
            }
            else
            {
                FromEmail = temp;
            }

            temp = Environment.GetEnvironmentVariable(TO_EMAIL_SETTING);
            if (temp == null)
            {
                Console.WriteLine("You must set your '" + TO_EMAIL_SETTING + "' environment variable.");
                isValid = false;
            }
            else
            {
                ToEmail = temp;
            }

            if(!isValid)
                Environment.Exit(0);
        }

        public bool SendEmail()
        {
            bool isValid = false;

            SmtpClient client = new SmtpClient(SMTPHost, 465);
            client.Credentials = new NetworkCredential(SMTPUsername, SMTPPassword);
            MailAddress from = new MailAddress(FromEmail, "Really Cool Display Name");
            MailAddress to = new MailAddress(ToEmail);
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test email";
            message.Subject = "This is a test subject";

            client.Send(message);
            message.Dispose();
            isValid = true;

            return isValid;
        }
    }
}
