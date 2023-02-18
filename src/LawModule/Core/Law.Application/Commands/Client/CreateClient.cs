using Law.Domain.Models;
using Law.Domain.Repositories;
using ShareServices.Events;
using ShareServices.Events.EventArgData;

namespace Law.Application.Commands.ClientC
{
    public class CreateClient
    {
        private readonly IClientEventService _clientEvent;
        private readonly IRepoWrapper _repo;

        public CreateClient(IClientEventService clientEvent, IRepoWrapper repo)
        {
            _clientEvent = clientEvent;
            _repo = repo;
            _clientEvent.CreatedClient += CreateClientHandler;
        }
        public async void CreateClientHandler(object? sender, CreatedUserArgs e)
        {
            var client = new Client() { Id = e.Id, Email = e.Email, FirstName = e.FirstName };
            var res = await _repo.ClientRepo.Add(client);
            _clientEvent.CreatedClient -= CreateClientHandler;
            if (!res.IsSuccess)
                throw new Exception(res.Errors());
        }
        //    private readonly IRepoWrapper _repo;
        //    private readonly string conString;
        //    private const string Chinnel = "createdClient";
        //    public CreateClient(IRepoWrapper repo, IOptionsSnapshot<RedisConfigModel> redisConfModelOpt)
        //    {
        //        _repo = repo;
        //        conString = redisConfModelOpt.Value.ConString;
        //    }
        //    private async void CreateClientHandle(CreatedUserDTO command)
        //    {
        //        var client = new LCS.Domain.Models.Client() { Id = command.Id, Email = command.Email, FirstName = command.FirstName };
        //       var res= await _repo.ClientRepo.Add(client);
        //        if (!res.IsSuccess)
        //            throw new Exception(res.ToString());
        //    }
        //    private void SubscribeToCreatedUser()
        //    {
        //        var connection = ConnectionMultiplexer.Connect(conString);
        //        connection.GetSubscriber().Subscribe(Chinnel, async (channel, message) =>
        //        {
        //            if (message.IsNull)
        //                throw new Exception("The Redis message subscribed to is null when trying to create client.");
        //            var res = JsonSerializer.Deserialize<CreatedUserDTO>(message!);
        //            if (res != null)
        //                CreateClientHandle(res);
        //            else
        //                throw new Exception("Created user is null when trying to create Client.");
        //        });
        //    }
    }
}
