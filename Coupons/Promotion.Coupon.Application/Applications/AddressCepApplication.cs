using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class AddressCepApplication : ApplicationBase<AddressCEP>, IAddressCepApplication
    {
        private readonly IAddressCepRepository _addressCepRepository;

        public AddressCepApplication()
        {
            _addressCepRepository = new AddressCepRepository();
        }
    }
}