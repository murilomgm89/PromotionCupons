using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IVoucherRepository : IRepositoryBase<Voucher>
    {
        Voucher GenarateWinner(int idPerson);
        int NumberCoupons();
    }
}