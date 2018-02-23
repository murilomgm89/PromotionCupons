using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface IBlockedCpfApplication //: IApplicationBase<BlockedCPFs>
    {
        bool IsCpfBlocked(string cpf);
    }
}