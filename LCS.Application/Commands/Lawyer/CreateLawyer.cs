using LCS.Application.Validations;
using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;


namespace LCS.Application.Commands.Lawyer
{
    public record CreateLawyer(Guid Id, string Email) : ICommand
    {
        public ActionResult Validate()
        {
            var res = new ActionResult();
            if (!Email.EmailValid())
            {
                res.AddError("Invalid Email.");
            }
            return res;
        }
    }

    public record CreateLawyerHandler : ICommandHandler<CreateLawyer>
    {
        public async Task<ActionResult> HandleAsync(CreateLawyer command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer = new LawyerTB() { Id = command.Id, Email = command.Email };
            return await repo.LawyerRepo.Add(lawyer);
        }
    }
}
