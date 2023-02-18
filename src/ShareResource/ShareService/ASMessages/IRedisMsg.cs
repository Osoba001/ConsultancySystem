using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.ASMessages
{
    public interface IRedisMsg
    {
        ValueTask<long> PublishAsync<T>(T record, string channel);

    }
}
