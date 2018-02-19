using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Areas.Admin.Models
{
    public class PersonDetailsViewModel
    {
        public Person Person { get; set; }
        public List<Receipt> Receipts { get; set; }
    }
}