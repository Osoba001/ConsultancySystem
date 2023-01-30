using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Commands.Lawyer
{
    public record RemoveWorkingSlot(List<Guid> Ids):ICommand;

    public class RemoveWorkingSlotHandler : ICommandHandler<RemoveWorkingSlot>
    {
        public async Task<ActionResult> HandleAsync(RemoveWorkingSlot command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.RemoveWorkingSlots(command.Ids);
        }
    }
}
