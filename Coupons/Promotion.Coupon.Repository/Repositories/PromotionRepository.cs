using System.Linq;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class PromotionRepository : RepositoryBase<Entity.Entities.Promotion>, IPromotionRepository
    {
        public IEnumerable<Entity.Entities.Promotion> GetByType(string type)
        {
            using (var context = new GymPass())
            {
                return context.Promotion
                    .Where(p => p.type == type)
                    .ToList();
            }
        }
    }
}