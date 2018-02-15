using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Login",
                "Admin/Login",
                new { controller = "Account", action = "Login" }
            );
        }
    }
}