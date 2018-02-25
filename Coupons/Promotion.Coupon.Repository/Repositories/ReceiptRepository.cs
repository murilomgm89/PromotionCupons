using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Poco;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class ReceiptRepository : RepositoryBase<Receipt>, IReceiptRepository
    {
        private static object _receiptSaveLock = new object();
        public Dictionary<string, int> GetCountPerDateBy(string productType, DateTime? @from = null, DateTime? to = null)
        {
            using (var context = new GymPass())
            {
                var query = context.Receipt.AsQueryable();

                //if (productType != null)
                //{
                //    //query = query.Where(r => r.Product.type == productType);
                //}

                if (from != null)
                {
                    query = query.Where(r => r.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.dtCreation <= to);
                }

                return query.GroupBy(g => g.dtCreation.Year + "-" + (g.dtCreation.Month < 10 ? ("0" + g.dtCreation.Month.ToString()) : g.dtCreation.Month.ToString()) + "-" + (g.dtCreation.Day < 10 ? ("0" + g.dtCreation.Day.ToString()) : g.dtCreation.Day.ToString())).ToDictionary(r => r.Key, r => r.Count());
            }
        }

        public Dictionary<string, int> GetCountPerDateBy2(string productType, DateTime? @from = null, DateTime? to = null)
        {
            using (var context = new GymPass())
            {
                var query = context.Receipt.AsQueryable();

                //if (productType != null)
                //{
                //    query = query.Where(r => r.Product.type == productType);
                //}

                if (from != null)
                {
                    query = query.Where(r => r.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.dtCreation <= to);
                }

                return query.GroupBy(g => g.dtCreation.Year + "-" + (g.dtCreation.Month < 10 ? ("0" + g.dtCreation.Month.ToString()) : g.dtCreation.Month.ToString()) + "-" + (g.dtCreation.Day < 10 ? ("0" + g.dtCreation.Day.ToString()) : g.dtCreation.Day.ToString())).ToDictionary(r => r.Key, r => r.Count());
            }
        }
        [Obsolete]
        public bool IsAllowedToSave(int idPerson, int idProduct, string cnpj)
        {
            throw new NotImplementedException();
        }

        public List<Receipt> GetReceiptsByIdPerson(int idPerson)
        {
            using (var context = new GymPass())
            {
                return context.Receipt
                    //.Include(r => r.LuckyCode)
                    .Include(r => r.Person)
                    //.Include(r => r.Product)
                    .Where(r => r.idPerson == idPerson)
                    .ToList();
            }
        }

        public List<Receipt> GetReceiptsBy(DateTime @from, DateTime to)
        {
            using (var context = new GymPass())
            {
                return context.Receipt
                    //.Include(r => r.LuckyCode)
                    .Include(r => r.Person)
                    //.Include(r => r.Product)
                    .Where(r => r.dtCreation >= from && r.dtCreation <= to)
                    .ToList();
            }
        }

        public List<ViewReceiptExport> GetReceiptsBy2(DateTime @from, DateTime to)
        {
            using (var context = new GymPass())
            {
                return context.ViewReceiptExport
                    .Where(v => v.Data_do_Cadastro_do_Recibo > from && v.Data_do_Cadastro_do_Recibo < to)
                    .ToList();
            }
        }

        public int GetCountByNotWinners()
        {
            using (var context = new GymPass())
            {
                return context.Receipt.Count(r => r.isValidated == true);
            }
        }

        public List<Receipt> GetReceiptsByWinners(string type)
        {
            using (var context = new GymPass())
            {
                var entity = context.Receipt
                    //.Where(r => r.isValidated == true && r.Product.type == type)
                    .Where(r => r.isValidated == true)
                    .ToList();

                return entity;
            }
        }

        public List<Receipt> GetBy(string type, bool isWinner, bool? isValidated)
        {
            using (var context = new GymPass())
            {
                return context.Receipt
                    .Include(rts => rts.Person)
                    //.Include(rts => rts.LuckyCode)
                    //.Include(rts => rts.Product)
                    .Where(r => r.isValidated == isValidated )
                    .OrderByDescending(r => r.dtCreation)
                    .ToList();
            }
        }

        public Receipt GetLastBy(int idPerson, string productType)
        { 
            using (var context = new GymPass())
            {
                return context.Receipt
                    .Where(r => r.idPerson == idPerson)
                    .OrderByDescending(r => r.dtCreation)
                    .FirstOrDefault();
            }
        }

        public Receipt GetById(int idReceipt)
        {
            using (var context = new GymPass())
            {
                return context.Receipt
                    .Include(r => r.Person)
                    //.Include(r => r.Product)
                    .Where(r => r.idReceipt == idReceipt)
                    .OrderByDescending(r => r.dtCreation)
                    .FirstOrDefault();
            }
        }

        public Receipt GetLastBy(bool isWinner, string productType)
        {
            using (var context = new GymPass())
            {
                return context.Receipt
                    //.Where(r => r.Product.type == productType)
                    .OrderByDescending(r => r.dtCreation)
                    .FirstOrDefault();
            }
        }

        public int GetCountBy(int idPerson, string productType)
        {
            using (var context = new GymPass())
            {
                return context.Receipt.Count(r => r.idPerson == idPerson);
            }
        }

        public int GetCountBy(string productType, bool isWinner)
        {
            using (var context = new GymPass())
            {
                return context.Receipt.Count(r => r.isValidated == isWinner);
            }
        }

        public int GetCountByParticipation(string productType)
        {
            using (var context = new GymPass())
            {
                return context.Receipt.Count();
            }
        }

        public int GetCountBy(DateTime dtSince, string productType = null, DateTime? dtUntil = null)
        {
            using (var context = new GymPass())
            {
                var query = context.Receipt.Where(r => r.dtCreation >= dtSince).AsQueryable();

                //if (productType != null)
                //{
                //    query = query.Where(r => r.Product.type == productType);
                //}

                if (dtUntil != null)
                {
                    query = query.Where(r => r.dtCreation <= dtUntil);
                }

                return query.Count();
            }
        }

        public int GetCountByAll(string productType = null)
        {
            using (var context = new GymPass())
            {
                //var query = context.Receipt.Where(r => r.Product.type == productType).ToList();
                //return query.Count();
                return 0;
            }
        }

        public void DeclaredWinner(Receipt receipt)
        {
            using (var context = new GymPass())
            {
                var entity = context.Receipt.FirstOrDefault(r => r.idReceipt == receipt.idReceipt);

                if (entity != null)
                    entity.isValidated = true;

                context.SaveChanges();
            }
        }

        public void Save(Receipt receipt, bool direct)
        {
            Insert(receipt);
        }

        public ReceiptSaveResult Save(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public void SetValidated(int idReceipt, bool isValidated, string invalidateDescription = null)
        {
            throw new NotImplementedException();
        }

        public void ApplyProductPromotion(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public int LuckyCodeRandom()
        {
            Random rnd = new Random();
            int luckyCode = rnd.Next(0, 99999);

            using (var context = new GymPass())
            {
                var obj = context.Voucher.Any(a => a.code == luckyCode);
                if (obj)
                {
                    luckyCode = rnd.Next(0, 99999);
                }
            }
            return luckyCode;
        }
    }
}