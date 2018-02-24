using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Areas.Admin.Models;
using Promotion.Coupon.Filters;

namespace Promotion.Coupon.Areas.Admin.Controllers
{
    [AdminAccessFilter]
    public class DashboardController : Controller
    {
        private readonly IReceiptApplication _receiptApplication;
        private readonly IPersonApplication _personApplication;
        public DashboardController()
        {
            _receiptApplication = new ReceiptApplication();
            _personApplication = new PersonApplication();
        }
        [GET("/admin/dashboard")]
        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            var vPowerData = _receiptApplication.GetCountPerDateBy("intimus");

            model.LineChartVPowerData = vPowerData.Select(d => new DashboardViewModel.ChartItem()
            {
                Label = d.Key,
                Value = d.Value
            })
            .OrderBy(d => d.Label)
            .ToList();

            //var vPowerData2 = _receiptApplication.GetCountByParticipation("intimus");

            model.PersonsChartData = _personApplication.GetCountPerDateBy(Convert.ToDateTime(vPowerData.OrderBy(k => k.Key).FirstOrDefault().Key), DateTime.Now)
                .Select(d => new DashboardViewModel.ChartItem()
                {
                    Label = d.Key,
                    Value = d.Value
                })
                .OrderBy(d => d.Label)
                .ToList();

            model.VPowerWinners = _receiptApplication.GetCountBy("intimus", true);

            return View("~/Areas/Admin/Views/Dashboard/Index.cshtml", model);
        }
    }
}