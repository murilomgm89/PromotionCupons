using System;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class AdminAccountApplication : ApplicationBase<AdminAccount>, IAdminAccountApplication
    {
        private readonly IAdminAccountRepository _adminAccountRepository;

        public AdminAccountApplication()
        {
            _adminAccountRepository = new AdminAccountRepository();
        }

        public AdminAccount GetByCredentials(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("Usuario e senha devem ser preenchidos.");

            return _adminAccountRepository.GetByCredentials(username, password);
        }
    } 
}