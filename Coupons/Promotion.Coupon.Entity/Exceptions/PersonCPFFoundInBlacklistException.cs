using System;

namespace Promotion.Coupon.Entity.Exceptions
{
    public class PersonCpfFoundInBlacklistException : Exception
    {
        public PersonCpfFoundInBlacklistException() : base("CPF encontrado na blacklist") { }

    }
}