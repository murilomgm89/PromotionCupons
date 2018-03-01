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
using System.Configuration;

namespace Promotion.Coupon.Areas.Admin.Controllers
{
    [AdminAccessFilter]
    public class ReceiptController : Controller
    {
        private readonly IReceiptApplication _receiptApplication;

        public ReceiptController()
        {
            _receiptApplication = new ReceiptApplication();
        }

        [GET("/admin/winners/gympass")]
        public ActionResult VPowerWinners()
        {
            var model = new VPowerWinnersViewModel();
            model.Approved = _receiptApplication.GetBy("intimus", true, true);
            model.Disapproved = _receiptApplication.GetBy("intimus", true, false);
            model.PendingVerification = _receiptApplication.GetBy("intimus", true, null);

            return View("~/Areas/Admin/Views/Receipt/VPowerWinners.cshtml", model);
        }

        public class ValidationViewModel
        {
            public bool isValidated { get; set; }
            public string invalidateDescription { get; set; }
            public string luckyNumber { get; set; }
            public int idReceipt { get; set; }
        }

        [GET("/admin/receipt/{idReceipt}/set-validated")]
        public ActionResult SetValidated(int idReceipt, ValidationViewModel model)
        {
            string desc = null;
            if (!String.IsNullOrEmpty(model.invalidateDescription))
            {
                desc = model.invalidateDescription;
            }

            var receipt = _receiptApplication.GetById(idReceipt);

            if (receipt != null)
            {
                string fileNews = "";
                if (model.isValidated == true)
                {
                    fileNews = Server.MapPath("~/Views/News/NewsVoucherView.cshtml");
                }
                else
                {
                    fileNews = Server.MapPath("~/Views/News/NewsCupomErrorView.cshtml");
                }

                _receiptApplication.SetValidated(idReceipt, model.isValidated, fileNews, ConfigurationManager.AppSettings["Ambiente"].ToString());
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}