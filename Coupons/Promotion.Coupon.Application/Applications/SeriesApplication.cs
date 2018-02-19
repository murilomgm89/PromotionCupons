using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class SeriesApplication : ApplicationBase<Series>, ISeriesApplication
    {
        private readonly ISeriesRepository _seriesRepository;

        public SeriesApplication()
        {
            _seriesRepository = new SeriesRepository();
        }
    }
}