using Law.Application.Commands.AppointmentB;
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
    public record BookOnlineAppointment: ICommand
    {
        public Guid ClientId { get; set; }
        public Guid LawyerId { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid TimeSlotId { get; set; }
        public string CaseDescription { get; set; }
        public string Language { get; set; }
        public ActionResult Validate()
        {
            ActionResult res = new();
            if (!ReviewDate.FutureDate())
            {
                res.AddError("Appointment date must be a future date.");
            }
            else if (!ReviewDate.FutureDate(DateTime.Now.AddMonths(1)))
            {
                res.AddError("Appointment date is tool high. It should not be more than 30 days.");
            }
            return res;
        }


    }

    public class BookOnlineAppointmentHandler : BookAppointmentBase, ICommandHandler<BookOnlineAppointment>
    {
        public async Task<ActionResult> HandleAsync(BookOnlineAppointment command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken = default)
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
            DateTime appointTime = command.ReviewDate.Date + tim;
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
                    AppointmentType = AppointmentType.Online,
                    CaseDescription = command.CaseDescription,
                    Language = command.Language,
                    Charge = lawyer.OnlineCharge
                };
                var res = await repo.AppointmentRepo.Add(appointment);
                if (res.IsSuccess)
                {
                    BookedAppointmentEmailSender(appointment, ServiceProvider);
                }
                return res;
            }
            else
                return repo.FailedAction("This lawyer just booked few seconds ago.");




        }
    }


}
