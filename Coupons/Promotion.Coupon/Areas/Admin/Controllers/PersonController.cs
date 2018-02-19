using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Areas.Admin.Models;

namespace Promotion.Coupon.Areas.Admin.Controllers
{
    public class PersonController : Controller
    {

        private readonly IReceiptApplication _receiptApplication;
        private readonly IPersonApplication _personApplication;

        public PersonController()
        {
            _receiptApplication = new ReceiptApplication();
            _personApplication = new PersonApplication();
        }
        [GET("/admin/person/{idPerson}")]
        public ActionResult PersonDetails(int idPerson)
        {
            PersonDetailsViewModel model = new PersonDetailsViewModel();

            model.Person = _personApplication.GetById(idPerson);
            model.Receipts = _receiptApplication.GetReceiptsByIdPerson(idPerson);

            return View("~/Areas/Admin/Views/Person/PersonDetails.cshtml", model);
        }
    }
}