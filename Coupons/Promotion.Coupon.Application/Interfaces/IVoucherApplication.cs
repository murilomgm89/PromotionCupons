using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;
namespace Promotion.Coupon.Application.Interfaces
{
    public interface IVoucherApplication
    {

        Voucher GenarateWinner(int idPerson);
        int NumberCoupons();
    }
}