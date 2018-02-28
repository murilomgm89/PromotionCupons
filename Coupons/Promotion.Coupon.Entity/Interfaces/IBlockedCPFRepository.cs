using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IBlockedCPFRepository : IRepositoryBase<BlockedCPF>
    {
        BlockedCPF GetCPF(long CPF);        
    }
}