using System;
using System.Collections.Generic;
using Promotion.Coupon.Application.Applications.Base;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories;

namespace Promotion.Coupon.Application.Applications
{
    public class LuckyCodeApplication : ApplicationBase<LuckyCode>, ILuckyCodeApplication
    {
        private readonly ILuckyCodeRepository _luckyCodeRepository;
        public LuckyCodeApplication()
        {
            _luckyCodeRepository = new LuckyCodeRepository();
        }

        public LuckyCode GetRandom()
        {
            return _luckyCodeRepository.GetRandom();
        }

        public void Save(LuckyCode luckyCode)
        {
            _luckyCodeRepository.Insert(luckyCode);
        }

        public LuckyCode GetBy(int luckyCode)
        {
            return _luckyCodeRepository.GetBy(luckyCode);
        }

        public IEnumerable<LuckyCode> GetByReceiptId(int idReceipt)
        {
            return _luckyCodeRepository.GetByReceiptId(idReceipt);
        }

        public Dictionary<string, int> GetCountPerDateBy(DateTime? @from = null, DateTime? to = null)
        {
            if (to == null)
                to = DateTime.Now;
            
            return _luckyCodeRepository.GetCountPerDateBy(from, to);
        }

        public int GetCountBy(DateTime dtSince, DateTime? dtUntil = null)
        {
            return _luckyCodeRepository.GetCountBy(dtSince, dtUntil);
        }

        public IEnumerable<LuckyCode> GetBy(DateTime @from, DateTime to)
        {
            return _luckyCodeRepository.GetBy(from, to);
        }

        public List<LuckyCode> GetWinnerMaiorLubrificantes(int numberCode)
        {
            return _luckyCodeRepository.GetWinnerMaiorLubrificantes(numberCode);
        }

        public List<LuckyCode> GetWinnerMenorLubrificantes(int numberCode)
        {
            return _luckyCodeRepository.GetWinnerMenorLubrificantes(numberCode);
        }
    }
}