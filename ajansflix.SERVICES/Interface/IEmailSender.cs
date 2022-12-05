using ajansflix.CORE.EmailConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(CustomerBook book);
    }
}
