﻿using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Controllers
{
    public class HomeController : Controller
    {
        [GET("/Admin")]
        public ActionResult Index()
        {
            return Redirect("/Admin/Login");
        }
    }
}