using System;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Enum;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface INewsSendingRepository : IRepositoryBase<NewsSending>
    {
        IEnumerable<NewsSending> GetToSend(int count);
        void SetToSend(string destination, ENewsType type, int? idReceipt = null, int? idPerson = null);
        [Obsolete]
        void Save(NewsSending news);
        string GetSubjectByNewsType(ENewsType type);
    }
}