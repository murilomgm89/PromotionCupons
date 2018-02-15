using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models
{
    public class WinnerListViewModel
    {
        public List<Entity.Receipt> VPowerWinners { get; set; }
        public List<Entity.Receipt> LubrificanteWinners { get; set; }
    }
}