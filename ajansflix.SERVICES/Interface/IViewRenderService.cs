using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
        Task<string> RenderToString(string viewName, object model);

    }
}
