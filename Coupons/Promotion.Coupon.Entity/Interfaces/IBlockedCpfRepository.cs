using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IBlockedCpfRepository : IRepositoryBase<BlockedCPFs>
    {
        bool IsCpfBlocked(string cpf);
    }
}