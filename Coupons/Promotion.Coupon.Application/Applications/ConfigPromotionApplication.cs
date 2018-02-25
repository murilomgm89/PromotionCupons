using System.Collections.Generic;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class ConfigPromotionApplication : ApplicationBase<ConfigPromotion>, IConfigPromotionApplication
    {
        private readonly IPromotionRepository _promotionRepository;

        public ConfigPromotionApplication()
        {
            _promotionRepository = new PromotionRepository();
        }
        public IEnumerable<ConfigPromotion> GetByType(string type)
        {
            return _promotionRepository.GetByType(type);
        }
    }
}