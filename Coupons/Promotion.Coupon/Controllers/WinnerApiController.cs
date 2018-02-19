using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Promotion.Coupon.Areas.Admin.Models;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Application.Applications;

namespace Promotion.Coupon.Controllers
{
    public class WinnerApiController : Controller
    {
        private readonly ILuckyCodeApplication _luckyCodeApplication;
        public WinnerApiController()
        {
            _luckyCodeApplication = new LuckyCodeApplication();
        }
        [GET("/api/lubrificantes/winners/{numberCode}")]
        public ActionResult GetWinnerLubrificantes(int numberCode)
        {
            var model = new LubrificantesWinnersViewModel
            {
                receiptsUP = _luckyCodeApplication.GetWinnerMaiorLubrificantes(numberCode),
                receiptsDown = _luckyCodeApplication.GetWinnerMenorLubrificantes(numberCode),
                codeSort = numberCode.ToString()
            };
            return View("~/Areas/Admin/Views/Shared/ListReceipts.cshtml", model);
        }
    }
}