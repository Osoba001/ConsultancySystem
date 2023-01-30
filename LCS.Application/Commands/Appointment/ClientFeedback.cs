using LCS.Domain.Constants;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Handlers.Appointment
{
    public record ClientFeedback(Guid AppointmentId, string Feedback, Star Star) : ICommand;

    public class ClientFeedbackHandler : ICommandHandler<ClientFeedback>
    {
        public async Task<ActionResult> HandleAsync(ClientFeedback command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var appointment = await repo.AppointmentRepo.GetById(command.AppointmentId);
            if (appointment != null)
            {
                appointment.Stars = command.Star;
                appointment.ClientFeedBack = command.Feedback;
                return await repo.AppointmentRepo.Update(appointment);
            }
            else
                return repo.FailedAction("Record not found.");
        }
    }
}
