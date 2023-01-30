using LCS.Domain.Constants;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Handlers.Appointment
{
    public record BookAppointment(Guid ClientId, Guid LawyerId, AppointmentType AppointmentType, DateTime reviewDate,
        Guid TimeSlotId, string CaseDescription, Guid LanguageId) : ICommand;

    public class BookAppointmentHandler : ICommandHandler<BookAppointment>
    {
        
        private static double GetCharge(LawyerTB lawyer, AppointmentType appointmentType)
        {
            if (appointmentType == AppointmentType.Online)
                return lawyer.OnlineCharge;
            return lawyer.OfflineCharge;
        }

        public async Task<ActionResult> HandleAsync(BookAppointment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var client = await repo.ClientRepo.GetById(command.ClientId);
            if (client != null)
            {
                var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
                if (lawyer != null)
                {
                    var slot = await repo.TimeSlotRepo.GetById(command.TimeSlotId);
                    if (slot != null)
                    {
                        var lang = await repo.LanguageRepo.GetById(command.LanguageId);
                        if (lang != null)
                        {
                            // checking if lawyer has been booked
                            var bookedLawyer = await repo.AppointmentRepo.FindOneByPredicate(x => x.Lawyer.Id == lawyer.Id &&
                            x.ReviewDate.Date == command.reviewDate.Date && x.TimeSlot.Id == slot.Id);
                            if (bookedLawyer != null)
                            {
                                var appointment = new AppointmentTB()
                                {
                                    Lawyer = lawyer,
                                    Client = client,
                                    TimeSlot = slot,
                                    ReviewDate = command.reviewDate,
                                    AppointmentType = command.AppointmentType,
                                    CaseDescription = command.CaseDescription,
                                    Charge = GetCharge(lawyer, command.AppointmentType)
                                };
                                return await repo.AppointmentRepo.Add(appointment);
                            }
                            else
                                return repo.FailedAction("This lawyer just booked few seconds ago.");
                        }
                        else
                            return repo.FailedAction("Language is not found.");

                    }
                    else
                        return repo.FailedAction("Time slot not found.");
                }
                else
                    return repo.FailedAction("Lawyer not found.");
            }
            else
                return repo.FailedAction("User not found.");
        }
    }
}
