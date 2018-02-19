using System.Linq;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public IEnumerable<Product> GetByType(string type)
        {
            using (var context = new GymPass())
            {
                return context.Product
                    .Where(p => p.type == type)
                    .ToList();
            }
        }
    }
}