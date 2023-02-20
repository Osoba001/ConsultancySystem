using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace Law.Application.Commands.Lawyer
{
    public record ReviewAppointment : ICommand
    {
        public Guid AppointmentId { get; set; }
        public string Report { get; set; }
        public ActionResult Validate()
        {
            ActionResult res = new ActionResult();
            if (!Report.StringMaxLength(500))
            {
                res.AddError("Feedback message is to long.");
            }
            return res;
        }
    }

    public class ReviewAppointmentHandler : ICommandHandler<ReviewAppointment>
    {
        public async Task<ActionResult> HandleAsync(ReviewAppointment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var appointment = await repo.AppointmentRepo.GetById(command.AppointmentId);
            if (appointment != null)
            {
                appointment.HasReviewed = true;
                appointment.LawyerReport = command.Report;
                return await repo.AppointmentRepo.Update(appointment);
            }
            else
                return repo.FailedAction("Record not found.");
        }
    }
}
