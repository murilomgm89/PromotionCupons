namespace Promotion.Coupon.Application.Interfaces
{
    public interface IVoucherApplication
    {

        void GenarateWinner(int idPerson);
        int NumberCoupons();
    }
}