using AttributeRouting.Web.Mvc;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Controllers
{
    public class PersonController : Controller
    {
        [GET("/admin/person/{idPerson}")]
        public ActionResult PersonDetails(int idPerson)
        {
            PersonDetailsViewModel model = new PersonDetailsViewModel();

            model.Person = Business.Person.GetById(idPerson);
            model.Receipts = Business.Receipt.GetReceiptsByIdPerson(idPerson);

            return View("~/Areas/Admin/Views/Person/PersonDetails.cshtml", model);
        }
    }
}