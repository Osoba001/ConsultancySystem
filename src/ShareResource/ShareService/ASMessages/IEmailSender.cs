using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.ASMessages
{
    public interface IEmailSender
    {
        ValueTask<bool> SendEmailAsync(string RecieverEmail, string subject, string body);
        ValueTask<bool> SendEmailAsync(List<string> RecieverEmail, string subject, string body);
    }
}
