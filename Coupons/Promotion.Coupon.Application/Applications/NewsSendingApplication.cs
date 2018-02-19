using System.Collections.Generic;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class NewsSendingApplication : ApplicationBase<NewsSending>, INewsSendingApplication
    {
        private readonly INewsSendingRepository _newsSendingRepository;

        public NewsSendingApplication()
        {
            _newsSendingRepository = new NewsSendingRepository();
        }

        public IEnumerable<NewsSending> GetToSend(int count)
        {
            throw new System.NotImplementedException();
        }

        public void SetToSend(string destination, ENewsType type, int? idReceipt = null, int? idPerson = null)
        {
            var data = new NewsSending()
            {
                Destination = destination,
                fileName = type.ToString(),
                Subject = GetSubjectByNewsType(type),
                Status = 0,
                idReceipt = idReceipt,
                idPerson = idPerson
            };

            if (data.idNewsSending.Equals(0))
                this.Insert(data);
            else
                this.Update(data);
            
        }

        public void Save(NewsSending news)
        {
            throw new System.NotImplementedException();
        }

        public string GetSubjectByNewsType(ENewsType type)
        {
            throw new System.NotImplementedException();
        }
    }
}