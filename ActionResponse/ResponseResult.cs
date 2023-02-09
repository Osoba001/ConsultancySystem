using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionResponse
{
    public class ResponseResult
    {
        public ResponseResult(bool succeded, StatusCode status)
        {
            Succeded = succeded;
            Status = status;
        }

        public bool Succeded { get; set; }
        public bool NotSucceded => !Succeded;
        public StatusCode Status { get; set; }
    }


    public class ResponseResult<TResult> : ResponseResult
    {
        public ResponseResult(bool succeded, StatusCode status) : base(succeded, status)
        {
        }
        public ResponseResult(TResult data, bool succeded, StatusCode status) : base(succeded, status) => Data = data;

        public TResult Data { get;}
    }
}
