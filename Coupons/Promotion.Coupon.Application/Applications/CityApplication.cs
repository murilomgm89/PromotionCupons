using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class CityApplication : ApplicationBase<City>, ICityApplication
    {
        private readonly ICityRepository _cityRepository;

        public CityApplication()
        {
            _cityRepository = new CityRepository();
        }
    }
}