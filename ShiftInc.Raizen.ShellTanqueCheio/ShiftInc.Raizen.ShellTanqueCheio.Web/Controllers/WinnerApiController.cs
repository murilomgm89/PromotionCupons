using AttributeRouting.Web.Mvc;
using ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions;
using ShiftInc.Raizen.ShellTanqueCheio.Entity;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Controllers
{
    public class WinnerApiController : Controller
    {
        [GET("/api/lubrificantes/winners/{numberCode}")]
        public ActionResult GetWinnerLubrificantes(int numberCode)
        {
            var model = new LubrificantesWinnersViewModel();
            List<Entity.LuckyCode> receipts = new List<Entity.LuckyCode>();
            model.receiptsUP = Business.Receipt.GetWinnerMaiorLubrificantes(numberCode);
            model.receiptsDown = Business.Receipt.GetWinnerMenorLubrificantes(numberCode);
            model.codeSort = numberCode.ToString();
            return View("~/Areas/Admin/Views/Shared/ListReceipts.cshtml", model);
        }
    }
}