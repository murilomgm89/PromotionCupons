using System;
using System.Linq;
using System.Runtime.InteropServices;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class BlokedCPFRepository : RepositoryBase<BlockedCPF>, IBlockedCPFRepository
    {
        public BlockedCPF GetCPF(long CPF)
        {
            using (var context = new GymPass())
            {
               return context.BlockedCPF.Where(b => b.CPF == CPF).FirstOrDefault();                
            }
        }
    }
}