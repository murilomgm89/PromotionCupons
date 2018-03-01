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
        private readonly IVoucherApplication _voucherApplication;
        public DashboardController()
        {
            _receiptApplication = new ReceiptApplication();
            _personApplication = new PersonApplication();
            _voucherApplication = new VoucherApplication();
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
            
            var PieVoucherDistribuido = _voucherApplication.NumberCoupons();
            var PieTotalVoucher = 5000;

            model.PieChartData = new List<DashboardViewModel.ChartItem>()
            {
                new DashboardViewModel.ChartItem() { Label = "Voucher Distribuídos", Value = PieVoucherDistribuido },
                new DashboardViewModel.ChartItem() { Label = "Quantidade de Voucher", Value = PieTotalVoucher }
            };

            var PieReprovados = _receiptApplication.GetBy("intimus", true, false).Count();
            var PiePendentes = _receiptApplication.GetBy("intimus", true, null).Count();

            model.PieChartData2 = new List<DashboardViewModel.ChartItem>()
            {
                new DashboardViewModel.ChartItem() { Label = "Voucher Distribuídos:", Value = PieVoucherDistribuido },
                new DashboardViewModel.ChartItem() { Label = "Reprovados", Value = PieReprovados },
                new DashboardViewModel.ChartItem() { Label = "Pendente de Cuaradoria", Value = PiePendentes }
            };     

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