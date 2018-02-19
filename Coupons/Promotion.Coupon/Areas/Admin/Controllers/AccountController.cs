using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Areas.Admin.Models;

namespace Promotion.Coupon.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAdminAccountApplication _adminAccountApplication;

        public AccountController()
        {
            _adminAccountApplication = new AdminAccountApplication();
        }
        [GET("/admin")]
        public ActionResult LoginAdmin()
        {
            return Redirect("~/admin/login");
        }
        [GET("/admin/login")]
        public ActionResult Login()
        {
            return View("~/Areas/Admin/Views/Account/Login.cshtml", new LoginViewModel());
        }
        [POST("/admin/login")]
        public ActionResult Login(LoginViewModel model)
        {
            var user = _adminAccountApplication.GetByCredentials(model.Username, model.Password);

            if (user == null)
            {
                model.Error = true;
                return View("~/Areas/Admin/Views/Account/Login.cshtml", model);
            }

            Session["Entity.AdminAccount"] = user;
            Session["Entity.AdminAccount.name"] = user.Username;

            return Redirect("/admin/dashboard");
        }
    }
}