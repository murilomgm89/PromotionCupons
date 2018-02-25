using System;
using System.Collections.Generic;
using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Poco;

namespace Promotion.Coupon.Application.Interfaces
{
    public interface IReceiptApplication : IApplicationBase<Receipt>
    {
        Dictionary<string, int> GetCountPerDateBy(string productType, DateTime? from = null, DateTime? to = null);
        Dictionary<string, int> GetCountPerDateBy2(string productType, DateTime? from = null, DateTime? to = null);
        bool IsAllowedToSave(int idPerson, int idProduct, string cnpj);
        List<Receipt> GetReceiptsByIdPerson(int idPerson);
        List<Receipt> GetReceiptsBy(DateTime from, DateTime to);
        List<ViewReceiptExport> GetReceiptsBy2(DateTime from, DateTime to);
        int GetCountByNotWinners();
        List<Receipt> GetReceiptsByWinners(string type);
        List<Receipt> GetBy(string type, bool isWinner, bool? isValidated);
        Receipt GetLastBy(int idPerson, string productType);
        Receipt GetById(int idReceipt);
        Receipt GetLastBy(bool isWinner, string productType);
        int GetCountBy(int idPerson, string productType);
        int GetCountBy(string productType, bool isWinner);
        int GetCountByParticipation(string productType);
        int GetCountBy(DateTime dtSince, string productType = null, DateTime? dtUntil = null);
        int GetCountByAll(string productType = null);
        void DeclaredWinner(Receipt receipt);
        void Save(Receipt receipt, bool direct);
        ReceiptSaveResult Save(Receipt receipt);
        void SetValidated(int idReceipt, bool isValidated, string invalidateDescription = null);
        void ApplyProductPromotion(Receipt receipt);
        int LuckyCodeRandom();

    }
}