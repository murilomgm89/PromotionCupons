using System.Collections.Generic;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class ProductApplication : ApplicationBase<Product>, IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication()
        {
            _productRepository = new ProductRepository();
        }

        public IEnumerable<Product> GetByType(string type)
        {
            return _productRepository.GetByType(type);
        }
    }
}