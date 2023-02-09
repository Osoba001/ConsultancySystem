using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messages
{
    public interface IRedisMessage
    {
        ValueTask<bool> PublishAsync<T>(T record, string channel);
    }
}
