using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record AddWorkingSlot(Guid LawyerId, List<Guid> TimeSlotIds) : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
    }

    public class AddWorkinSltHandler : ICommandHandler<AddWorkingSlot>
    {
        public async Task<ActionResult> HandleAsync(AddWorkingSlot command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.AddSWorkingSlots(command.LawyerId, command.TimeSlotIds);
        }
    }
}
