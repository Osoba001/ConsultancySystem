using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ShareServices.Models;



namespace ShareServices.ASMessages
{
    public class EmaiKitlSender : IEmailSender
    {
        private readonly EmailConfigData emailConfigData;
        public EmaiKitlSender(IOptionsSnapshot<EmailConfigData> emaildataOpt)
        {
            emailConfigData= emaildataOpt.Value;
        }
        public async ValueTask<bool> SendEmailAsync(string recieverEmail, string subject, string body)
        {
            var recieverEmails = new List<string> { recieverEmail };
            return await SendEmailAsync(recieverEmails, subject, body);
        }

        public async ValueTask<bool> SendEmailAsync(List<string> recieverEmails, string subject, string body)
        {
            if (!recieverEmails.Any())
                return false;
            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(emailConfigData.SenderEmail));
            foreach (string email in recieverEmails)
            {
                msg.To.Add(MailboxAddress.Parse(email));
            }

            msg.Subject = subject;
            msg.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smt = new SmtpClient();
            smt.Connect(emailConfigData.Host, emailConfigData.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smt.Authenticate(emailConfigData.SenderEmail, emailConfigData.SenderPassword);
            bool res = false;
            try
            {
                await smt.SendAsync(msg);
                res = true;
            }
            catch
            {

            }
            finally
            {
                await smt.DisconnectAsync(true);
                smt.Dispose();
            }
            return res;
        }
    }
}
