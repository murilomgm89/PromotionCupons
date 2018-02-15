using ShiftInc.Raizen.ShellTanqueCheio.Business.Repository;
using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business
{
    public class LuckyCode
    {
        public static Entity.LuckyCode GetRandom()
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.LuckyCodes.Where(lc => lc.idReceipt == null).OrderBy(lc => lc.idLuckyCode);

                return query.Skip(new Random().Next(query.Count())).FirstOrDefault();
            }
        }

        public static void Save(Entity.LuckyCode luckyCode)
        {
            ShiftIncRepository.Save<Entity.LuckyCode>(luckyCode);
        }

        public static Entity.LuckyCode GetBy(int luckyCode)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.LuckyCodes.Where(lc => lc.code == luckyCode).FirstOrDefault();
            }
        }

        public static IEnumerable<Entity.LuckyCode> GetByReceiptId(int idReceipt)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.LuckyCodes.Include(lc => lc.Receipt).Where(lc => lc.idReceipt == idReceipt).ToList();
            }
        }

        public static Dictionary<string, int> GetCountPerDateBy(DateTime? from = null, DateTime? to = null)
        {
            if (to == null)
            {
                to = DateTime.Now;
            }

            var response = new Dictionary<string, int>();

            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.LuckyCodes.AsQueryable();

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

        public static int GetCountBy(DateTime dtSince, DateTime? dtUntil = null)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.LuckyCodes.Where(r => r.Receipt.dtCreation >= dtSince).AsQueryable();
                
                if (dtUntil != null)
                {
                    query = query.Where(r => r.Receipt.dtCreation <= dtUntil);
                }

                return query.Count();
            }
        }

        public static IEnumerable<Entity.LuckyCode> GetBy(DateTime from, DateTime to)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.LuckyCodes.Include(p => p.Receipt).Where(p => p.Receipt.dtCreation >= from && p.Receipt.dtCreation <= to).ToList();
            }
        }
    }
}
