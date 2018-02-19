using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class AddressApplication : ApplicationBase<Address>, IAddressApplication
    {
        private readonly IAddressRepository _addressRepository;

        public AddressApplication()
        {
            _addressRepository = new AddressRepository();
        }
    }
}