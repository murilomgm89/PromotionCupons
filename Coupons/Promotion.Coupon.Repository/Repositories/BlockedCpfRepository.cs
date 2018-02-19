using System.Linq;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class BlockedCpfRepository : RepositoryBase<BlockedCPFs>, IBlockedCpfRepository
    {
        public bool IsCpfBlocked(string cpf)
        {
            using (var context = new GymPass())
            {
                return context.BlockedCPFs.Any(b => b.CPF == cpf);
            }
        }
    }
}