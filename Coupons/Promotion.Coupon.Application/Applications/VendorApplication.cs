using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class VendorApplication : ApplicationBase<Vendor>, IVendorApplication
    {
        private readonly IVendorRepository _vendorRepository;
        public VendorApplication()
        {
            _vendorRepository = new VendorRepository();
        }
    }
}