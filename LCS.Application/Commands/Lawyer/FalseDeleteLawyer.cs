using Law.Domain.Repositories;
using ShareServices.Events;

namespace Law.Application.Commands.Lawyer
{
    public class FalseDeleteLawyer
    {
        private readonly ILawyerEventService _lawyerEvent;
        private readonly IRepoWrapper _repo;
        public FalseDeleteLawyer(ILawyerEventService lawyerEvent, IRepoWrapper repo)
        {
            _lawyerEvent = lawyerEvent;
            _repo = repo;
            _lawyerEvent.FalseDeletedLawyer += FalseDeleteLawyerHandle;
        }

        public async void FalseDeleteLawyerHandle(object? sender, UserIdArgs e)
        {
            var user = await _repo.LawyerRepo.GetById(e.Id);
            if (user != null)
            {
                user.IsDelete = true;
                var res = await _repo.LawyerRepo.Update(user);
                _lawyerEvent.FalseDeletedLawyer -= FalseDeleteLawyerHandle;
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }
            else
                throw new Exception("user you're trying to delete is not found.");
        }
    }


}
