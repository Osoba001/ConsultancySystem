using LCS.Domain.Models;
using LCS.WebApi.Response.Base;
namespace LCS.WebApi.Response;

public class TimeSlotResponse : BaseResponse
{
    public int Index { get; set; }
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }

    public string  StringValue { get; set; }

    public static implicit operator TimeSlotResponse(TimeSlot model)
    {
        return new TimeSlotResponse()
        {
            Id = model.Id,
            Index = model.Index,
            StartHour = model.StartHour,
            StartMinute = model.StartMinute,
            EndHour = model.EndHour,
            EndMinute = model.EndMinute,
            CreatedDate = model.CreatedDate,
            StringValue = model.ToString()
        };
    }
}
