using Law.Domain.Repositories;
using ShareServices.Events;

namespace Law.Application.Commands.Lawyer
{
    public class DeleteLawyer
    {
        private readonly ILawyerEventService _lawyerEvent;
        private readonly IRepoWrapper _repo;
        public DeleteLawyer(ILawyerEventService lawyerEvent, IRepoWrapper repo)
        {
            _lawyerEvent = lawyerEvent;
            _repo = repo;
            _lawyerEvent.HardDeletedLawyer += DeleteLawyerEventHandle;
        }

        public async void DeleteLawyerEventHandle(object? sender, UserIdArgs userIdArgs)
        {
            var res = await _repo.LawyerRepo.Delete(userIdArgs.Id);
            _lawyerEvent.HardDeletedLawyer -= DeleteLawyerEventHandle;
            if (!res.IsSuccess)
                throw new Exception(res.Errors());
        }
    }

}
