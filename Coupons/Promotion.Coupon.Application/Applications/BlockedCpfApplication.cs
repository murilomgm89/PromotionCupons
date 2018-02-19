using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class BlockedCpfApplication : ApplicationBase<BlockedCPFs>, IBlockedCpfApplication
    {
        private readonly IBlockedCpfRepository _blockedCpfRepository;

        public BlockedCpfApplication()
        {
            _blockedCpfRepository = new BlockedCpfRepository();
        }

        public bool IsCpfBlocked(string cpf)
        {
            return _blockedCpfRepository.IsCpfBlocked(cpf);
        }
    }
}