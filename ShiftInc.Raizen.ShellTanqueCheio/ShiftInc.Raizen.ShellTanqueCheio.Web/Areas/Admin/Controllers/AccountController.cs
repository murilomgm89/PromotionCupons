using AttributeRouting.Web.Mvc;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        [GET("/admin")]
        public ActionResult LoginAdmin()
        {
            return Redirect("/admin/login");
        }

        [GET("/admin/login")]
        public ActionResult Login()
        {
            return View("~/Areas/Admin/Views/Account/Login.cshtml", new LoginViewModel());
        }

        [POST("/admin/login")]
        public ActionResult Login(LoginViewModel model)
        {
            var user = Business.AdminAccount.GetByCredentials(model.Username, model.Password);

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