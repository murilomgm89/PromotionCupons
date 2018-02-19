using System.Web.Mvc;

namespace Promotion.Coupon.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Redirect("/Admin/Login");
        }
    }
}