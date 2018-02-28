using System;
using System.Linq;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Poco;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Exceptions;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Repository.Repositories;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Entity.Handle;

namespace Promotion.Coupon.Application.Applications
{
    public class BlockedCPFApplication : ApplicationBase<BlockedCPF>, IBlockedCpfApplication
    {       
       
        private readonly IBlockedCPFRepository _blockedCPFRepository;

        public BlockedCPFApplication()
        {
            _blockedCPFRepository = new BlokedCPFRepository();           
        }

        public BlockedCPF GetCPF(long CPF)
        {
            return _blockedCPFRepository.GetCPF(CPF);
        }
    }
}