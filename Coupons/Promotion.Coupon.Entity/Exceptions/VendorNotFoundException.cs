using System;

namespace Promotion.Coupon.Entity.Exceptions
{
    public class VendorNotFoundException : Exception
    {
        public VendorNotFoundException() : base("O CNPJ não foi encontrado na base de postos autorizados") { }
    }
}