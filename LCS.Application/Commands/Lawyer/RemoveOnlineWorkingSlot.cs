using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record RemoveOnlineWorkingSlot(Guid LawyerId, List<Guid> SlotIds) : ICommand
    {
        public ActionResult Validate() => new();
    }

    public class RemoveOnlineWorkingSlotHandler : ICommandHandler<RemoveOnlineWorkingSlot>
    {
        public async Task<ActionResult> HandleAsync(RemoveOnlineWorkingSlot command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer is not null)
            {
                var mySlots = lawyer.OnlineWorkingSlots.IntersectBy(command.SlotIds, p => p.Id);
                foreach (var slot in mySlots)
                {
                    lawyer.OnlineWorkingSlots.Remove(slot);
                }
                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User is not found!");
        }
    }


}
