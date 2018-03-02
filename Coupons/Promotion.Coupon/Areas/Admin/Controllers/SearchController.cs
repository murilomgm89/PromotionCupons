using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {


        private readonly IPersonApplication _personApplication;

        public SearchController()
        {
            _personApplication = new PersonApplication();
        }
        [GET("/admin/search")]
        public ActionResult Search()
        {
            var model = new SearchResultViewModel();

            model.Search = "";
            if (Request.QueryString["search"] != null)
            {
                model.Search = Request.QueryString["search"];
            }
            model.ReceiptList = _personApplication.GetBySearch(model.Search).ToList();

            return View("~/Areas/Admin/Views/Search/Search.cshtml", model);
        }
    }

    public class SearchResultViewModel
    {
        public List<Person> PersonList { get; set; }
        public string Search { get; set; }
        public List<Receipt> ReceiptList { get; set; }
    }
}
