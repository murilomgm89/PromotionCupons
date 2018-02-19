using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> GetByType(string type);
    }
}