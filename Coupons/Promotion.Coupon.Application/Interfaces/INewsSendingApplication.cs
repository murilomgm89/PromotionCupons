using System;
using System.Collections.Generic;
using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Enum;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface INewsSendingApplication : IApplicationBase<NewsSending>
    {
        IEnumerable<NewsSending> GetToSend(int count);
        void SetToSend(string destination, ENewsType type, int? idReceipt = null, int? idPerson = null);
        [Obsolete]
        void Save(NewsSending news);
        string GetSubjectByNewsType(ENewsType type);
    }
}