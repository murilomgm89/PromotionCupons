using AttributeRouting.Web.Mvc;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Controllers
{
    [AdminAccessFilter]
    public class ReceiptController : Controller
    {
        [GET("/admin/winners/gympass")]
        public ActionResult VPowerWinners()
        {
            var model = new VPowerWinnersViewModel();
            model.Approved = Business.Receipt.GetBy("intimus", true, true);
            model.Disapproved = Business.Receipt.GetBy("intimus", true, false);
            model.PendingVerification = Business.Receipt.GetBy("intimus", true, null);

            return View("~/Areas/Admin/Views/Receipt/VPowerWinners.cshtml", model);
        }

        public class ValidationViewModel
        {
            public bool isValidated {get;set;}
            public string invalidateDescription {get;set;}
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

            var receipt = Business.Receipt.GetById(idReceipt);

            if (receipt != null)
            {
                Business.Receipt.SetValidated(idReceipt, model.isValidated, desc);
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }  
    }
}