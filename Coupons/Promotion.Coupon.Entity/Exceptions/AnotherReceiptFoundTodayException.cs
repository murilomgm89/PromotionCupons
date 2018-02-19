using System;

namespace Promotion.Coupon.Entity.Exceptions
{
    public class AnotherReceiptFoundTodayException : Exception
    {
        public AnotherReceiptFoundTodayException() : base("Outro cupom desse tipo já foi adicionado hoje, o usuário deve tentar novamente amanhã") { }
    }
}