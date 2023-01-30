using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;


namespace LCS.Application.Commands.Lawyer
{
    public record CreateLawyer(Guid Id, string Email) : ICommand;

    public record CreateLawyerHandler : ICommandHandler<CreateLawyer>
    {
        public async Task<ActionResult> HandleAsync(CreateLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer = new LawyerTB() { Id = command.Id, Email = command.Email };
            return await repo.LawyerRepo.Add(lawyer);
        }
    }
}
