using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class VoucherApplication : ApplicationBase<Voucher>, IVoucherApplication
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherApplication()
        {
            _voucherRepository = new VoucherRepository();
        }
        public Voucher GenarateWinner(int idPerson)
        {
            return _voucherRepository.GenarateWinner(idPerson);
        }

        public int NumberCoupons()
        {
            return _voucherRepository.NumberCoupons();
        }
    }
}