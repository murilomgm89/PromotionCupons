using System.Web.Mvc;

namespace Promotion.Coupon.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "Login",
                "Admin/Login",
                new { controller = "Account", action = "Login" }
            );

            //context.MapRoute(
            //    name: "Admin Padrao",
            //    url: "Admin/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}