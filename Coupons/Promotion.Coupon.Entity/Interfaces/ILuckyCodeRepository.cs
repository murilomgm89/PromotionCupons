using System;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;

namespace Promotion.Coupon.Entity.Interfaces
{
    public interface ILuckyCodeRepository : IRepositoryBase<LuckyCode>
    {
        LuckyCode GetRandom();
        [Obsolete]
        void Save(LuckyCode luckyCode);
        LuckyCode GetBy(int luckyCode);
        IEnumerable<LuckyCode> GetByReceiptId(int idReceipt);
        Dictionary<string, int> GetCountPerDateBy(DateTime? from = null, DateTime? to = null);
        int GetCountBy(DateTime dtSince, DateTime? dtUntil = null);
        IEnumerable<LuckyCode> GetBy(DateTime from, DateTime to);
        List<LuckyCode> GetWinnerMaiorLubrificantes(int numberCode);
        List<LuckyCode> GetWinnerMenorLubrificantes(int numberCode);
    }
}