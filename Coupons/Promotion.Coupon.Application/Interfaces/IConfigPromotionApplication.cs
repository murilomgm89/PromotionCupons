﻿using System.Collections.Generic;
using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface IConfigPromotionApplication : IApplicationBase<ConfigPromotion>
    {
        IEnumerable<Entity.Entities.ConfigPromotion> GetByType(string type);
    }
}