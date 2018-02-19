using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Areas.Admin.Models
{
    public class VPowerWinnersViewModel
    {
        public IEnumerable<Receipt> PendingVerification { get; set; }
        public IEnumerable<Receipt> Approved { get; set; }
        public IEnumerable<Receipt> Disapproved { get; set; }
    }
}