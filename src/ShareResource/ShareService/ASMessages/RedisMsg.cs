using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShareServices.ASMessages
{
    public class RedisMsg : IRedisMsg
    {
        private readonly IConfiguration _config;

        public RedisMsg(IConfiguration config)
        {
            _config = config;
        }
        public async ValueTask<bool> PublishAsync<T>(T record, string channel)
        {
            var jsonData = JsonSerializer.Serialize(record);
            var conString = _config.GetConnectionString("Redis");
            var connection = ConnectionMultiplexer.Connect(conString);
            return await connection.GetSubscriber().PublishAsync(channel, jsonData) > 0;
        }

        public async void SubscribeAsync<T>(string channel, Action<T> action)
        {
            var conString = _config.GetConnectionString("Redis");
            var connection = ConnectionMultiplexer.Connect(conString);
            await connection.GetSubscriber().SubscribeAsync(channel, async (channel, message) =>
            {
                var res = JsonSerializer.Deserialize<T>(message);
                action(res);
            });
        }
        //public void MessageHandlandler(RedisValue message)
        //{
        //    var res = JsonSerializer.Deserialize<T>(message);
        //}
    }
}
