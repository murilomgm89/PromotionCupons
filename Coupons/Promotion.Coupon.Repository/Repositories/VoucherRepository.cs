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
        public void GenarateWinner(int idPerson)
        {
            var code = _receiptRepository.LuckyCodeRandom();
            var data = new Voucher()
            {
                idPerson = idPerson,
                code = code,
                dtWinner = DateTime.Now
            };
            using (var context = new GymPass())
            {

                context.Voucher.Add(data);
                context.SaveChanges();
            }
        }

        public int NumberCoupons()
        {
            using (var context = new GymPass())
            {
                return context.Voucher.ToList().Count;
            }
        }
    }
}