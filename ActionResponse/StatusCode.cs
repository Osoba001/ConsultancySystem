using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ActionResponse
{
    public class StatusCode
    {
        public StatusCode(string message, HttpStatusCode statusCode=HttpStatusCode.BadRequest)
        {
            Code = statusCode;
            Message = message;
        }
        public static StatusCode InvalidArg => new (nameof(InvalidArg));
        public static StatusCode ErrorWhileSavingToDatabase => new (string.Empty,HttpStatusCode.InternalServerError);
        public static StatusCode FileSizeLimit => new(nameof(FileSizeLimit));
        public static StatusCode UnsupportedFileType => new(nameof(UnsupportedFileType),HttpStatusCode.UnsupportedMediaType);
        public static StatusCode EmptyFile => new(nameof(EmptyFile));
        public static StatusCode OK200 => new(_ok,HttpStatusCode.OK);
        public static StatusCode OK201 => new(_ok,HttpStatusCode.Created);
        public static StatusCode OK202 => new(_ok,HttpStatusCode.Accepted);
        public static StatusCode OK204 => new(_ok,HttpStatusCode.NoContent);
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }

        private static readonly string _ok = "Success";

        public override string ToString()
        {
            return $"StatusCode: '{Code}', Message:'{Message}'.";
        }
    }
   
}
