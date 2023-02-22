using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record AddOfflineWorkingSlot : ICommand
    {
        public Guid LawyerId { get; set; }
        public List<Guid> TimeSlotIds { get; set; }
        public ActionResult Validate() => new();
    }

    public class AddOfflineWorkinSlotHandler : ICommandHandler<AddOfflineWorkingSlot>
    {
        public async Task<ActionResult> HandleAsync(AddOfflineWorkingSlot command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer is not null)
            {
                var Allslots = await repo.TimeSlotRepo.GetAll();
                var mySlots = Allslots.IntersectBy(command.TimeSlotIds, p => p.Id);
                foreach (var slot in mySlots)
                {
                    if (!lawyer.OfflineWorkingSlots.Where(x => x.Id == slot.Id).Any())
                        lawyer.OfflineWorkingSlots.Add(slot);
                }
                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User is not found!");
        }
    }
}
