using Law.Application.Response.Base;
using Law.Domain.Models;

namespace Law.Application.Response;

public class TimeSlotResponse : BaseResponse
{
    public int Index { get; set; }

    public string StringValue { get; set; }

    public static implicit operator TimeSlotResponse(TimeSlot model)
    {
        return new TimeSlotResponse()
        {
            Id = model.Id,
            Index = model.Index,
            StringValue = model.ToString()
        };
    }
}
