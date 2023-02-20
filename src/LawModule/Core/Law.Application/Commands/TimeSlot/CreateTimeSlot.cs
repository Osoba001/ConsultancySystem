using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.TimeSlot
{
    public class CreateTimeSlot : ICommand
    {
        public ActionResult Validate() => new();
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
        private static List<Domain.Models.TimeSlot> GenerateTimeSlot()
        {
            List<Domain.Models.TimeSlot> slots = new();
            for (int i = 0; i < 24; i++)
            {
                Domain.Models.TimeSlot slot = new()
                {
                    StartHour = i,
                    StartMinute = 0,
                    EndHour = i,
                    EndMinute = 30,
                    Index = i * 2 + 1
                };
                slots.Add(slot);
                Domain.Models.TimeSlot slot2 = new()
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
