using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ShareServices.Models;
using ShareServices.RedisMsgDTO;
using System.Reflection;
using System.Xml.Linq;

namespace ShareServices.EmailService
{
    public class EmaiKitlSender : IEmailSender
    {
        private readonly EmailConfigData emailConfigData;
        public EmaiKitlSender(IOptionsSnapshot<EmailConfigData> emaildataOpt)
        {
            emailConfigData = emaildataOpt.Value;
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
            bool res = false;
            //var msg = new MimeMessage();
            //msg.From.Add(MailboxAddress.Parse(emailConfigData.SenderEmail));
            //foreach (string email in recieverEmails)
            //{
            //    msg.To.Add(MailboxAddress.Parse(email));
            //}

            //msg.Subject = subject;
            //msg.Body = new TextPart(TextFormat.Html) { Text = body };

            //using var smt = new SmtpClient();
            //smt.Connect(emailConfigData.Host, emailConfigData.Port, MailKit.Security.SecureSocketOptions.StartTls);
            //smt.Authenticate(emailConfigData.SenderEmail, emailConfigData.SenderPassword);
            
            //try
            //{
            //    await smt.SendAsync(msg);
            //    res = true;
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    await smt.DisconnectAsync(true);
            //    smt.Dispose();
            //}
            return res;
        }

        public async void SendOnBookedAppointmentEmailAsync(BookedAppointmentDTO appointment)
        {
            string subjet = "Consultancy Appointment.";
            string bodyForReceiver = $"Hi {appointment.Receiver},\nYou have an appointment to review with {appointment.Client} on {appointment.ReviewDate:yyyymmdd_hhmm}.\nKeep to time.\nThanks for trusting our service.";
            await SendEmailAsync(appointment.ReceiverEmail, subjet, bodyForReceiver);

            string bodyClient = $"Hi {appointment.Receiver},\nYou have booked an appointment with {appointment.Receiver} to be review on {appointment.ReviewDate:yyyymmdd_hhmm}.\nKeep to time.\nThanks for trusting our service.";
            await SendEmailAsync(appointment.ClientEmail, subjet, bodyClient);
        }

        public async void SendRecoverPinEmailAsync(string RecieverEmail, int pin)
        {
            string subjet = "Consultancy Password Recovering code.";
            string body = $"Below is your password recovery Code.\nRecovery code: {pin}\n Thanks for trusting our service.";
            await SendEmailAsync(RecieverEmail, subjet, body);
        }
    }
}
