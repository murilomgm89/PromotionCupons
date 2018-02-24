using System.Linq;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class PromotionRepository : RepositoryBase<ConfigPromotion>, IPromotionRepository
    {
        public IEnumerable<ConfigPromotion> GetByType(string type)
        {
            using (var context = new GymPass())
            {
                return context.ConfigPromotion
                    .Where(p => p.type == type)
                    .ToList();
            }
        }
    }
}