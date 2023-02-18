using Microsoft.Extensions.Options;
using ShareServices.ASMessages;
using ShareServices.Events;
using ShareServices.Events.EventArgData;
using ShareServices.Models;
using ShareServices.RedisMsgDTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShareServices.Services
{
    internal class EmailServices
    {
        private readonly IEmailSender _emailSender;
        private readonly IAppointmentEvent _appointmentEvent;
        private readonly string conString;
        public EmailServices(IEmailSender emailSender, IOptionsSnapshot<RedisConfigModel> redisConfModelOpt, IAppointmentEvent appointmentEvent)
        {
            _emailSender = emailSender;
            _appointmentEvent = appointmentEvent;
            conString = redisConfModelOpt.Value.ConString;
            _appointmentEvent.BookedAppointmentEvent += BookedAppointment;
        }

        private void BookedAppointment(object? sender, BookedAppointmentEventArg e)
        {
            string subjet = "Consultancy Appointment.";
            string bodyForReceiver = $"Hi {e.Receiver},\nYou have an appointment to review with {e.Client} on {e.ReviewDate:yyyymmdd_hhmm}.\nKeep to time.\nThanks for trusting our service.";
            _emailSender.SendEmailAsync(e.ReceiverEmail, subjet, bodyForReceiver);

            string bodyClient = $"Hi {e.Receiver},\nYou have booked an appointment with {e.Receiver} to be review on {e.ReviewDate:yyyymmdd_hhmm}.\nKeep to time.\nThanks for trusting our service.";
            _emailSender.SendEmailAsync(e.ClientEmail, subjet, bodyClient);

            _appointmentEvent.BookedAppointmentEvent -= BookedAppointment;
        }

        private void OnForgetPassword()
        {
            var connection = ConnectionMultiplexer.Connect(conString);
            connection.GetSubscriber().Subscribe("forgetPassword", async (channel, message) =>
           {
               if (message.IsNull)
                   throw new NullReferenceException($"The Redis message subscribed to is null on forget password.");
               var dto = JsonSerializer.Deserialize<RecoveringPasswordDTO>(message!);
               if (dto != null)
                   SendRecoveringPasswordEmail(dto);
               else
                   throw new NullReferenceException("Recovering password dto is null.");
           });
        }
        private void SendRecoveringPasswordEmail(RecoveringPasswordDTO dto)
        {
            string subjet = "Consultancy Password Recovering code.";
            string body = $"Hi {dto.Name},\nBelow is your password recovery Code.\nRecovery code: {dto.Pin}\n Thanks for trusting our service.";
            _emailSender.SendEmailAsync(dto.Email, subjet, body);
        }
        //private void OnBookedAppointment()
        //{
        //    var connection = ConnectionMultiplexer.Connect(conString);
        //    connection.GetSubscriber().Subscribe("bookedAppointmen", async (channel, message) =>
        //    {
        //        if (message.IsNull)
        //            throw new NullReferenceException($"The Redis message subscribed to is null on booked appointment.");
        //        var dto = JsonSerializer.Deserialize<BookedAppointmentDTO>(message!);
        //        if (dto != null)
        //            SendBookedAppointmentEmail(dto);
        //        else
        //            throw new NullReferenceException("Booked appointment dto is null.");
        //    });
        //}
        //private void SendBookedAppointmentEmail(BookedAppointmentDTO dto)
        //{
        //    string subjet = "Consultancy Appointment.";
        //    string bodyForReceiver = $"Hi {dto.Receiver},\nYou have an appointment to review with {dto.Client} on {dto.ReviewDate.ToString("yyyymmdd_hhmm")}.\nKeep to time.\nThanks for trusting our service.";
        //    _emailSender.SendEmailAsync(dto.ReceiverEmail, subjet, bodyForReceiver);

        //    string bodyClient = $"Hi {dto.Receiver},\nYou have booked an appointment with {dto.Receiver} to be review on {dto.ReviewDate.ToString("yyyymmdd_hhmm")}.\nKeep to time.\nThanks for trusting our service.";
        //    _emailSender.SendEmailAsync(dto.ClientEmail, subjet, bodyClient);
        //}
        


    }
}
