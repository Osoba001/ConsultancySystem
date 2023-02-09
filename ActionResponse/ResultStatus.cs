using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionResponse
{
    public static class ResultStatus
    {
        public static ResponseResult Pass(StatusCode code) => new(true, code);
        public static ResponseResult Fail(StatusCode code) => new(false, code);
    }

    public static class ResultStatus<TResult>
    {
        public static ResponseResult<TResult> Pass(TResult response, StatusCode code)=>new(response,true,code);
        public static ResponseResult<TResult> Pass(StatusCode code)=>new(true,code);
        public static ResponseResult<TResult> Fail(StatusCode code)=>new(false,code);
    }
}
