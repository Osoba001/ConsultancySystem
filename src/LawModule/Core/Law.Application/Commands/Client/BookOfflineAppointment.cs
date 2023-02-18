using Law.Domain.Constants;
using Law.Domain.Models;
using Law.Domain.Repositories;
using ShareServices.Events;
using ShareServices.Events.EventArgData;
using SimpleMediatR.MediatRContract;

using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace Law.Application.Commands.ClientC
{
    public record BookOfflineAppointment(Guid ClientId, Guid LawyerId, DateTime reviewDate,
       Guid TimeSlotId, string CaseDescription, string Language) : ICommand
    {
        public ActionResult Validate()
        {
            ActionResult res = new();
            if (!reviewDate.FutureDate())
            {
                res.AddError("Appointment date must be a future date.");
            }
            else if (!reviewDate.FutureDate(DateTime.Now.AddMonths(1)))
            {
                res.AddError("Appointment date is tool high. It should not be more than 30 days.");
            }
            return res;
        }
    }
    public class BookOfflineAppointmentHandler : ICommandHandler<BookOfflineAppointment>, IAppointmentEvent
    {
        public event EventHandler<BookedAppointmentEventArg>? BookedAppointmentEvent;

        protected virtual void OnBookedAppointment(BookedAppointmentEventArg arg)
        {
            BookedAppointmentEvent?.Invoke(this, arg);
        }
        public async Task<ActionResult> HandleAsync(BookOfflineAppointment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var client = await repo.ClientRepo.GetById(command.ClientId);
            if (client == null)
                return repo.FailedAction("User not found.");

            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer == null)
                return repo.FailedAction("Lawyer not found.");
            var slot = await repo.TimeSlotRepo.GetById(command.TimeSlotId);
            if (slot == null)
                return repo.FailedAction("Time slot not found.");

            TimeSpan tim = new(slot.StartHour, slot.StartMinute, 0);
            DateTime appointTime = command.reviewDate.Date + tim;
            var bookedLawyer = await repo.AppointmentRepo.FindOneByPredicate(x => x.Lawyer.Id == lawyer.Id &&
            x.ReviewDate == appointTime);
            if (bookedLawyer == null)
            {

                var appointment = new Appointment()
                {
                    Lawyer = lawyer,
                    Client = client,
                    TimeSlot = slot,
                    ReviewDate = appointTime,
                    AppointmentType = AppointmentType.Offline,
                    CaseDescription = command.CaseDescription,
                    Charge = lawyer.OfflineCharge,
                    Language = command.Language
                };
                var res= await repo.AppointmentRepo.Add(appointment);
                if (res.IsSuccess)
                {
                    var bkAppAgr = new BookedAppointmentEventArg
                    {
                        AppointmentName = "Offline",
                        Receiver = lawyer.FirstName,
                        Client = client.FirstName,
                        ReceiverEmail = lawyer.Email,
                        ClientEmail = lawyer.Email,
                        ReviewAddress = $"{lawyer.OfficeAddress},{lawyer.Location}, {lawyer.State}",
                        ReviewDate = appointTime,
                    };
                    OnBookedAppointment(bkAppAgr);
                }
                return res;
            }
            else
                return repo.FailedAction("This lawyer just booked few seconds ago.");

        }
    }

}
