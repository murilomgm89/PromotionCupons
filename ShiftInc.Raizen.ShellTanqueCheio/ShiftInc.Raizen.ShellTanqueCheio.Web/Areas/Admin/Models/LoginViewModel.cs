using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Error { get; set; }
    }
}