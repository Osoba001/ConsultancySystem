using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.TimeSlot
{
    public record CreateTimeSlot() : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
    }

    public class CreateTimeSlotHandler : ICommandHandler<CreateTimeSlot>
    {
        public async Task<ActionResult> HandleAsync(CreateTimeSlot command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var slots = await repo.TimeSlotRepo.GetAll();
            if (slots.Count > 0)
                return repo.TimeSlotRepo.FailedAction("Slot already exist.");
            return await repo.TimeSlotRepo.AddRange(GenerateTimeSlot());
        }
        private static List<TimeSlotTB> GenerateTimeSlot()
        {
            List<TimeSlotTB> slots = new();
            for (int i = 0; i < 24; i++)
            {
                TimeSlotTB slot = new()
                {
                    StartHour = i,
                    StartMinute = 0,
                    EndHour = i,
                    EndMinute = 30,
                    Index = i * 2 + 1
                };
                slots.Add(slot);
                TimeSlotTB slot2 = new()
                {
                    StartHour = i,
                    StartMinute = 30,
                    EndHour = i + 1,
                    EndMinute = 0,
                    Index = i * 2 + 2
                };
                slots.Add(slot2);
            }
            return slots;
        }

        
    }
}
