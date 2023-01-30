using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Lawyer
{
    public record RemoveLawyerFrmDepartment(Guid LawyerDeptId) : ICommand;

    public record RemoveLawyerFrmDepartmentHandler : ICommandHandler<RemoveLawyerFrmDepartment>
    {
        public async Task<ActionResult> HandleAsync(RemoveLawyerFrmDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.LawyerRepo.LeaveDepartment(command.LawyerDeptId);
        }
    }
}
