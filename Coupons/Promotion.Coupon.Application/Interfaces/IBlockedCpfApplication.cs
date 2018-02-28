using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface IBlockedCpfApplication : IApplicationBase<BlockedCPF>
    {
        BlockedCPF GetCPF(long CPF);
    }
}