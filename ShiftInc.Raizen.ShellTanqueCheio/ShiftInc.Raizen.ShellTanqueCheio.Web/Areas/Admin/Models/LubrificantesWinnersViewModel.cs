using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models
{
    public class LubrificantesWinnersViewModel
    {
        public IEnumerable<Entity.Receipt> Winners { get; set; }
        public IEnumerable<Entity.Receipt> Approved { get; set; }
        public IEnumerable<Entity.Receipt> Disapproved { get; set; }
        public List<Entity.LuckyCode> receiptsUP { get; set; }
        public List<Entity.LuckyCode> receiptsDown { get; set; }
        public string codeSort { get; set; }
    }
}