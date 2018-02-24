using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Areas.Admin.Models
{
    public class LubrificantesWinnersViewModel
    {
        public IEnumerable<Receipt> Winners { get; set; }
        public IEnumerable<Receipt> Approved { get; set; }
        public IEnumerable<Receipt> Disapproved { get; set; }
        //public List<LuckyCode> receiptsUP { get; set; }
        //public List<LuckyCode> receiptsDown { get; set; }
        public string codeSort { get; set; }
    }
}