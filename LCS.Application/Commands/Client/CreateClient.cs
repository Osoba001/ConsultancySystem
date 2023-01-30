using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Client
{
    public record CreateClient(Guid Id, string Email) : ICommand;

    public class CreateClientHandler : ICommandHandler<CreateClient>
    {
        public async Task<ActionResult> HandleAsync(CreateClient command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.ClientRepo.Add(new ClientTB() { Id = command.Id, Email = command.Email });
        }
    }
}
