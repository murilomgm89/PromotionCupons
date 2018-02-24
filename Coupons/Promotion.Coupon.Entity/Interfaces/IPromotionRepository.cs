using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IPromotionRepository : IRepositoryBase<Entities.ConfigPromotion>
    {
        IEnumerable<Entities.ConfigPromotion> GetByType(string type);
    }
}