using LCS.Domain.Models;
using LCS.WebApi.Response.Base;

namespace LCS.WebApi.Response
{
    public class LanguageResponse : BaseResponse
    {
        public string Name { get; set; }
        public static implicit operator LanguageResponse(Language model)
        {
            return new LanguageResponse() { Id = model.Id, Name = model.Name };
        }
    }
}
