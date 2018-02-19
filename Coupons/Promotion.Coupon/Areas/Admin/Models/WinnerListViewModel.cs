using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Areas.Admin.Models
{
    public class WinnerListViewModel
    {
        public List<Receipt> VPowerWinners { get; set; }
        public List<Receipt> LubrificanteWinners { get; set; }
    }
}