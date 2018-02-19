using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IAdminAccountRepository : IRepositoryBase<AdminAccount>
    {
        AdminAccount GetByCredentials(string username, string password);
    }
}