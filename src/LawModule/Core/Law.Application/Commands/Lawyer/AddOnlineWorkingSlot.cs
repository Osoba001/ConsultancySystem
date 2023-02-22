using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record AddOnlineWorkingSlot : ICommand
    {
        public Guid LawyerId { get; set; }
        public List<Guid> TimeSlotIds { get; set; }
        public ActionResult Validate() => new();
    }

    public class AddOnlineWorkinSlotHandler : ICommandHandler<AddOnlineWorkingSlot>
    {
        public async Task<ActionResult> HandleAsync(AddOnlineWorkingSlot command, IRepoWrapper repo, IServiceProvider ServiceProvider, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(command.LawyerId);
            if (lawyer is not null)
            {
                var Allslots = await repo.TimeSlotRepo.GetAll();
                var mySlots = Allslots.IntersectBy(command.TimeSlotIds, p => p.Id);
                foreach (var slot in mySlots)
                {
                    if (!lawyer.OnlineWorkingSlots.Where(x => x.Id == slot.Id).Any())
                        lawyer.OnlineWorkingSlots.Add(slot);
                }
                return await repo.LawyerRepo.Update(lawyer);
            }
            else
                return repo.FailedAction("User is not found!");
        }
    }


}
