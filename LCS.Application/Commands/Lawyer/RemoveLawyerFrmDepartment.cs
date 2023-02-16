using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.Lawyer
{
    public record RemoveLawyerFrmDepartment(Guid LawyerId, Guid DeptId) : ICommand
    {
        public ActionResult Validate() => new();
    }

    public record RemoveLawyerFrmDepartmentHandler : ICommandHandler<RemoveLawyerFrmDepartment>
    {
        public async Task<ActionResult> HandleAsync(RemoveLawyerFrmDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.GetById(command.DeptId);
            if (dept != null)
            {
                var lawyer = dept.Lawyers.Where(x => x.Id == command.LawyerId).FirstOrDefault();
                if (lawyer != null)
                {
                    dept.Lawyers.Remove(lawyer);
                    return await repo.DepartmentRepo.Update(dept);
                }
                else
                    return repo.FailedAction("User is not found.");
            }
            else
                return repo.FailedAction("Department is not found!");
        }
    }
}
