using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class LuckyCodeRepository : RepositoryBase<LuckyCode>, ILuckyCodeRepository
    {
        public LuckyCode GetRandom()
        {
            using (var context = new GymPass())
            {
                var query = context.LuckyCode.Where(lc => lc.idReceipt == null).OrderBy(lc => lc.idLuckyCode);

                return query.Skip(new Random().Next(query.Count())).FirstOrDefault();
            }
        }
        [Obsolete]
        public void Save(LuckyCode luckyCode)
        {
            Insert(luckyCode);
        }

        public LuckyCode GetBy(int luckyCode)
        {
            using (var context = new GymPass())
            {
                return context.LuckyCode.FirstOrDefault(lc => lc.code == luckyCode);
            }
        }

        public IEnumerable<LuckyCode> GetByReceiptId(int idReceipt)
        {
            using (var context = new GymPass())
            {
                return context.LuckyCode
                    .Include(lc => lc.Receipt)
                    .Where(lc => lc.idReceipt == idReceipt)
                    .ToList();
            }
        }

        public Dictionary<string, int> GetCountPerDateBy(DateTime? @from = null, DateTime? to = null)
        {
            Dictionary<string, int> response;

            using (var context = new GymPass())
            {
                var query = context.LuckyCode.AsQueryable();

                if (from != null)
                {
                    query = query.Where(r => r.Receipt.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.Receipt.dtCreation <= to);
                }

                response = query.GroupBy(g => g.Receipt.dtCreation.Year + "-" + (g.Receipt.dtCreation.Month < 10 ? ("0" + g.Receipt.dtCreation.Month.ToString()) : g.Receipt.dtCreation.Month.ToString()) + "-" + (g.Receipt.dtCreation.Day < 10 ? ("0" + g.Receipt.dtCreation.Day.ToString()) : g.Receipt.dtCreation.Day.ToString())).ToDictionary(r => r.Key, r => r.Count());
            }

            DateTime aux = DateTime.Now;

            if (from == null)
            {
                string strfrom = response.Min(r => r.Key);
                aux = new DateTime(Convert.ToInt32(strfrom.Split('-')[0]), Convert.ToInt32(strfrom.Split('-')[1]), Convert.ToInt32(strfrom.Split('-')[2]));
            }
            else
            {
                aux = new DateTime(from.Value.Year, from.Value.Month, from.Value.Day);
            }

            while (aux < to)
            {
                if (response.ContainsKey(aux.ToString("yyyy-MM-dd")))
                {

                }
                else
                {
                    response.Add(aux.ToString("yyyy-MM-dd"), 0);
                }

                aux = aux.AddDays(1);
            }

            return response;
        }

        public int GetCountBy(DateTime dtSince, DateTime? dtUntil = null)
        {
            using (var context = new GymPass())
            {
                var query = context.LuckyCode
                    .Where(r => r.Receipt.dtCreation >= dtSince)
                    .AsQueryable();

                if (dtUntil != null)
                    query = query.Where(r => r.Receipt.dtCreation <= dtUntil);


                return query.Count();
            }
        }

        public IEnumerable<LuckyCode> GetBy(DateTime @from, DateTime to)
        {
            using (var context = new GymPass())
            {
                return context.LuckyCode
                    .Include(p => p.Receipt)
                    .Where(p => p.Receipt.dtCreation >= from && p.Receipt.dtCreation <= to)
                    .ToList();
            }
        }

        public List<LuckyCode> GetWinnerMaiorLubrificantes(int numberCode)
        {
            using (var context = new GymPass())
            {
                return context.LuckyCode
                    .Include(lc => lc.Receipt)
                    .Include(lc => lc.Receipt.Person)
                    .Where(lc => lc.code >= numberCode)
                    .OrderBy(lc => lc.code)
                    .Take(10)
                    .ToList();
            }
        }

        public List<LuckyCode> GetWinnerMenorLubrificantes(int numberCode)
        {
            using (var context = new GymPass())
            {
                return context.LuckyCode
                    .Include(lc => lc.Receipt)
                    .Include(lc => lc.Receipt.Person)
                    .Where(lc => lc.code <= numberCode)
                    .OrderByDescending(lc => lc.code)
                    .Take(10)
                    .ToList();
            }
        }
    }
}