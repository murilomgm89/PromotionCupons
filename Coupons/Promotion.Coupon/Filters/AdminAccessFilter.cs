using System.Web.Mvc;

namespace Promotion.Coupon.Filters
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