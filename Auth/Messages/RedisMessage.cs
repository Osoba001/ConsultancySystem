using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auth.Messages
{
    internal class RedisMessage : IRedisMessage
    {
        private readonly IConfiguration _config;

        public RedisMessage(IConfiguration config)
        {
            _config = config;
        }
        public async ValueTask<bool> PublishAsync<T>(T record, string channel)
        {
            {
                var jsonData = JsonSerializer.Serialize(record);
                var conString = _config.GetConnectionString("Redis");
                var connection = ConnectionMultiplexer.Connect(conString);
                return await connection.GetSubscriber().PublishAsync(channel, jsonData) > 0;
            }
        }
    }
}
