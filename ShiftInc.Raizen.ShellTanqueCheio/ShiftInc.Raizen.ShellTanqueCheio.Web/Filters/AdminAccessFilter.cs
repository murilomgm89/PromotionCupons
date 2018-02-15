using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Filters
{
    public class AdminAccessFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Entity.AdminAccount"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/admin/login");
            }
        }
    }
}