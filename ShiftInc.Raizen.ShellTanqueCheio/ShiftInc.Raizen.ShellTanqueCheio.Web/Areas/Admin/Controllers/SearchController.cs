using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        [GET("/admin/search")]
        public ActionResult Search()
        {
            var model = new SearchResultViewModel();

            model.Search = "";
            if (Request.QueryString["search"] != null)
            {
                model.Search = Request.QueryString["search"];
            }
            model.PersonList = Business.Person.GetBySearch(model.Search).ToList();

            return View("~/Areas/Admin/Views/Search/Search.cshtml", model);
        }
    }

    public class SearchResultViewModel
    {
        public List<Entity.Person> PersonList { get; set; }
        public string Search { get; set; }
    }
}