using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Models
{
    public class ReceiptSaveViewModel
    {
        public string Status { get; set; }
        public Receipt Receipt { get; set; }
        public List<Receipt> Receipts { get; set; }
    }
}