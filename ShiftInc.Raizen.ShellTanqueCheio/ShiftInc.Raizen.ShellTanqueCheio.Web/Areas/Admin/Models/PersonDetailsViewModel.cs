using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Areas.Admin.Models
{
    public class PersonDetailsViewModel
    {
        public Entity.Person Person { get; set; }
        public List<Entity.Receipt> Receipts { get; set; }
    }
}