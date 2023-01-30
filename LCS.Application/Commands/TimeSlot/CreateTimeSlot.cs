using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.TimeSlot
{
    public record CreateTimeSlot() : ICommand;

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
                TimeSlotTB slot = new();
                slot.StartHour = i;
                slot.StartMinute = 0;
                slot.EndHour = i;
                slot.EndMinute = 30;
                slot.Index = i * 2 + 1;
                slots.Add(slot);
                TimeSlotTB slot2 = new();
                slot2.StartHour = i;
                slot2.StartMinute = 30;
                slot2.EndHour = i + 1;
                slot2.EndMinute = 0;
                slot2.Index = i * 2 + 2;
                slots.Add(slot2);
            }
            return slots;
        }

        
    }
}
