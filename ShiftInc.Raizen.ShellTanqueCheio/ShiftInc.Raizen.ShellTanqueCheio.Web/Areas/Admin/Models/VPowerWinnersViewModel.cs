using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models
{
    public class VPowerWinnersViewModel
    {
        public IEnumerable<Entity.Receipt> PendingVerification { get; set; }
        public IEnumerable<Entity.Receipt> Approved { get; set; }
        public IEnumerable<Entity.Receipt> Disapproved { get; set; }
    }
}