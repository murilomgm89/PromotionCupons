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
        public void GenarateWinner(int idPerson)
        {
            _voucherRepository.GenarateWinner(idPerson);
        }

        public int NumberCoupons()
        {
            return _voucherRepository.NumberCoupons();
        }
    }
}