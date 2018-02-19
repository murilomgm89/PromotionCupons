using System.Linq;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class AdminAccountRepository : RepositoryBase<AdminAccount>, IAdminAccountRepository
    {
        public AdminAccount GetByCredentials(string username, string password)
        {
            using (var context = new GymPass())
            {
                return context.AdminAccount.FirstOrDefault(aa => aa.Username == username && aa.Password == password);
            }
        }
    }
}