using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface IAdminAccountApplication : IApplicationBase<AdminAccount>
    {
        AdminAccount GetByCredentials(string username, string password);
    }
}