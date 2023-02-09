using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record AddLawyerToDepartment(Guid LawyerId, Guid DeptId) : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
    }

    public record AddLawyerToDepartmentHandler : ICommandHandler<AddLawyerToDepartment>
    {
       
        public async Task<ActionResult> HandleAsync(AddLawyerToDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.JoinDepartment(command.LawyerId, command.DeptId);
        }
    }
}
