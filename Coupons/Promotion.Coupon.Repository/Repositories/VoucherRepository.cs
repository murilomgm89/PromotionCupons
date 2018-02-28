using System;
using System.Linq;
using System.Runtime.InteropServices;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class VoucherRepository : RepositoryBase<Voucher>, IVoucherRepository
    {
        private readonly IReceiptRepository _receiptRepository;

        public VoucherRepository()
        {
            _receiptRepository = new ReceiptRepository();
        }
        public Voucher GenarateWinner(int idPerson)
        {            
            using (var context = new GymPass())
            {
                var voucher = context.Voucher.Where(v => v.idPerson == null).OrderBy(v => v.code).First();
                if (voucher != null)
                {
                    voucher.idPerson = idPerson;                    
                    voucher.dtWinner = DateTime.Now;
                }
                context.SaveChanges();
                return voucher;
            }
        }

        public int NumberCoupons()
        {
            using (var context = new GymPass())
            {
                return context.Voucher.Where(v => v.idPerson != null).ToList().Count;
            }
        }
    }
}