using Law.Domain.Repositories;
using ShareServices.Events;

namespace Law.Application.Commands.Lawyer
{
    public class CreateLawyer
    {
        private readonly ILawyerEventService _lawyerEvent;
        private readonly IRepoWrapper _repo;

        public CreateLawyer(ILawyerEventService lawyerEvent, IRepoWrapper repo)
        {
            _lawyerEvent = lawyerEvent;
            _repo = repo;
            _lawyerEvent.CreatedLawyer += CreateLawyerHandler;
        }
        public async void CreateLawyerHandler(object? sender, CreatedUserArgs e)
        {
            var lawyer = new Law.Domain.Models.Lawyer() { Id = e.Id, Email = e.Email, FirstName = e.FirstName };
            var res = await _repo.LawyerRepo.Add(lawyer);
            _lawyerEvent.CreatedLawyer -= CreateLawyerHandler;
            if (!res.IsSuccess)
                throw new Exception(res.Errors());
        }
        //private readonly IRepoWrapper _repo;
        //private readonly string conString;
        //private const string CreatedLawyerChinnel = "createdLawyer";
        //public CreateLawyer(IRepoWrapper repo, IOptionsSnapshot<RedisConfigModel> redisConfModelOpt)
        //{
        //    _repo = repo;
        //    conString = redisConfModelOpt.Value.ConString;
        //}
        //private void CreateLwayerHandle(CreatedUserDTO command)
        //{
        //    var lawyer = new LCS.Domain.Models.Lawyer() { Id = command.Id, Email = command.Email, FirstName = command.FirstName };
        //  _repo.LawyerRepo.Add(lawyer);
        //}
        //private async void SubscribeToCreatedUser()
        //{
        //    var connection = ConnectionMultiplexer.Connect(conString);
        //    await connection.GetSubscriber().SubscribeAsync(CreatedLawyerChinnel, async (channel, message) =>
        //    {
        //        var res = JsonSerializer.Deserialize<CreatedUserDTO>(message);
        //        if (res != null)
        //            CreateLwayerHandle(res);
        //        else
        //            throw new Exception("Created user is null when trying to create lawyer.");
        //    });
        //}
    }
}
