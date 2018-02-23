using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IPromotionRepository : IRepositoryBase<Promotion>
    {
        IEnumerable<Promotion> GetByType(string type);
    }
}