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
    public class DashboardController : Controller
    {
        [GET("/admin/dashboard")]
        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            var vPowerData = Business.Receipt.GetCountPerDateBy("intimus");
            
            model.LineChartVPowerData = vPowerData.Select(d => new ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models.DashboardViewModel.ChartItem() { Label = d.Key, Value = d.Value }).OrderBy(d => d.Label).ToList();

            var vPowerData2 = Business.Receipt.GetCountByParticipation("intimus");    
            
            model.PersonsChartData = Business.Person.GetCountPerDateBy(Convert.ToDateTime(vPowerData.OrderBy(k => k.Key).FirstOrDefault().Key), DateTime.Now).Select(d => new ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models.DashboardViewModel.ChartItem() { Label = d.Key, Value = d.Value }).OrderBy(d => d.Label).ToList();

            model.VPowerWinners = Business.Receipt.GetCountBy("intimus", true);

            return View("~/Areas/Admin/Views/Dashboard/Index.cshtml", model);
        }
    }
}