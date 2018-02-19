using System;

namespace Promotion.Coupon.Entity.Exceptions
{
    public class ReceiptCountPerPersonExceededException : Exception
    {
        public ReceiptCountPerPersonExceededException() : base("Já foram cadastrados para esse CPF o número máximo de cupons permitidos") { }
    }
}