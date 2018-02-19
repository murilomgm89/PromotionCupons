using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class SequenceSeriesApplication : ApplicationBase<SequenceSeries>, ISequenceSeriesApplication
    {
        private readonly ISequenceSeriesRepository _sequenceSeriesRepository;

        public SequenceSeriesApplication()
        {
            _sequenceSeriesRepository = new SequenceSeriesRepository();
        }
    }
}