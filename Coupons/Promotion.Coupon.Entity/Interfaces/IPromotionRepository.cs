using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IPromotionRepository : IRepositoryBase<Entities.Promotion>
    {
        IEnumerable<Entities.Promotion> GetByType(string type);
    }
}