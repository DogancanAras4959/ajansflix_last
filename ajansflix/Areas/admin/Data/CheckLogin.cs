using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ajansflix.Areas.admin.Data
{
    public class CheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("account") == null)
            {
                filterContext.Result = new RedirectResult("/admin/editor/login");
            }
            //base.OnActionExecuted(filterContext);
        }
    }
}
