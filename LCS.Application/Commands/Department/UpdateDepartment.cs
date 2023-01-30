using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Department
{
    public record UpdateDepartment(Guid Id, string Description) : ICommand;

    public class UpdateDepartmentHandler : ICommandHandler<UpdateDepartment>
    {
        public async Task<ActionResult> HandleAsync(UpdateDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.GetById(command.Id);
            if (dept is not null)
            {
                dept.Description = command.Description;
                return await repo.DepartmentRepo.Update(dept);
            }
            else
                return repo.DepartmentRepo.FailedAction("Record does not exist!");
        }
    }

}
