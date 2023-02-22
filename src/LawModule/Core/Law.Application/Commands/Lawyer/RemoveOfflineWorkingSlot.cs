using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record RemoveOfflineWorkingSlot: ICommand
    {
        public Guid LawyerId { get; set; }
        public List<Guid> SlotIds { get; set; }
        public ActionResult Validate() => new();
    }

    public class RemoveOfflineWorkingSlotHandler : ICommandHandler<RemoveOfflineWorkingSlot>
    {
        public async Task<ActionResult> HandleAsync(RemoveOfflineWorkingSlot command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer is not null)
            {
                var mySlots = lawyer.OfflineWorkingSlots.IntersectBy(command.SlotIds, p => p.Id);
                foreach (var slot in mySlots)
                {
                    lawyer.OfflineWorkingSlots.Remove(slot);
                }
                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User is not found!");
        }

    }
}
