using ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions;
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
    public class Receipt
    {
        public static Dictionary<string, int> GetCountPerDateBy(string productType, DateTime? from = null, DateTime? to = null)
        {
            if (to == null)
            {
                to = DateTime.Now;
            }

            if (from == null)
            {
                from = DateTime.Now.AddDays(-9);
            }           
            var response = new Dictionary<string, int>();

            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.Receipts.AsQueryable();

                if(productType != null) {
                    query = query.Where(r => r.Product.type == productType);
                }

                if (from != null)
                {
                    query = query.Where(r => r.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.dtCreation <= to);
                }

                response = query.GroupBy(g => g.dtCreation.Year + "-" + (g.dtCreation.Month < 10 ? ("0" + g.dtCreation.Month.ToString()) : g.dtCreation.Month.ToString()) + "-" + (g.dtCreation.Day < 10 ? ("0" + g.dtCreation.Day.ToString()) : g.dtCreation.Day.ToString())).ToDictionary(r => r.Key, r => r.Count());
            }

            DateTime aux = DateTime.Now;

            if (from == null)
            {
                string strfrom = response.Min(r => r.Key) ?? DateTime.Now.ToString("yyyy-MM-dd");
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


        public static Dictionary<string, int> GetCountPerDateBy2(string productType, DateTime? from = null, DateTime? to = null)
        {
            from = DateTime.Now.AddDays(-2);  
            if (to == null)
            {
                to = DateTime.Now;
            }

            var response = new Dictionary<string, int>();

            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.Receipts.AsQueryable();

                if (productType != null)
                {
                    query = query.Where(r => r.Product.type == productType);
                }

                if (from != null)
                {
                    query = query.Where(r => r.dtCreation >= from);
                }

                if (to != null)
                {
                    query = query.Where(r => r.dtCreation <= to);
                }

                response = query.GroupBy(g => g.dtCreation.Year + "-" + (g.dtCreation.Month < 10 ? ("0" + g.dtCreation.Month.ToString()) : g.dtCreation.Month.ToString()) + "-" + (g.dtCreation.Day < 10 ? ("0" + g.dtCreation.Day.ToString()) : g.dtCreation.Day.ToString())).ToDictionary(r => r.Key, r => r.Count());
            }

            DateTime aux = DateTime.Now;

            if (from == null)
            {
                string strfrom = response.Min(r => r.Key) ?? DateTime.Now.ToString("yyyy-MM-dd");
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


        private static bool IsAllowedToSave(int idPerson, int idProduct, string cnpj)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var product = ShiftIncRepository.GetById<Entity.Product>(idProduct);

                var lastReceipt = GetLastBy(idPerson, product.type);
                bool isLastReceiptNotToday = lastReceipt == null || (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime(lastReceipt.dtCreation.Year, lastReceipt.dtCreation.Month, lastReceipt.dtCreation.Day));
                if (!isLastReceiptNotToday)
                {
                    throw new AnotherReceiptFoundTodayException();
                }

                var countReceipts = GetCountBy(idPerson, product.type);
                bool isReceiptCountUnderLimit = countReceipts < (product.type == "v-power" ? 30 : 5);
                if (!isReceiptCountUnderLimit)
                {
                    throw new ReceiptCountPerPersonExceededException();
                }

                return true;
            }
        }

        public static List<Entity.Receipt> GetReceiptsByIdPerson(int idPerson)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts
                    .Include(r => r.LuckyCodes)
                    .Include(r => r.Person)
                    .Include(r => r.Product)
                    .Where(r => r.idPerson == idPerson)                    
                    .ToList();
            }
        }

        public static List<Entity.Receipt> GetReceiptsBy(DateTime from, DateTime to)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts
                    .Include(r => r.LuckyCodes)
                    .Include(r => r.Person) 
                    .Include(r => r.Product)
                    .Where(r => r.dtCreation >= from && r.dtCreation <= to)                   
                    .ToList();
            }
        }

        public static List<Entity.ViewReceiptExport> GetReceiptsBy2(DateTime from, DateTime to)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.ViewReceiptExport.Where(v => v.Data_do_Cadastro_do_Recibo > from && v.Data_do_Cadastro_do_Recibo < to).ToList(); 
            }
        }

        public static int GetCountByNotWinners()
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Where(r => r.isValidated == true).Count();
            }
        }

        public static List<Entity.Receipt> GetReceiptsByWinners(string type)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var entity = context.Receipts.Where(r => r.isValidated == true && r.Product.type == type).ToList();


                    //entity.OrderByDescending(r => r.Person.name).ToList();
                return entity;
            }
        }

        public static List<Entity.Receipt> GetBy(string type, bool isWinner, bool? isValidated)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Include(rts => rts.Person)
                    .Include(rts => rts.LuckyCodes)                    
                   
                    .Include(rts => rts.Product)
                    .Include(rts => rts.LuckyCodes)
                    .Where(r => r.isValidated == isValidated && r.Product.type == type).OrderByDescending(r => r.dtCreation).ToList();
            }
        }

        public static Entity.Receipt GetLastBy(int idPerson, string productType)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Where(r => r.idPerson == idPerson && r.Product.type == productType).OrderByDescending(r => r.dtCreation).FirstOrDefault();
            }
        }

        public static Entity.Receipt GetById(int idReceipt)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Include(r => r.Person)
                    .Include(r => r.Product)
                    .Include(r => r.Product)
                    .Where(r => r.idReceipt == idReceipt).OrderByDescending(r => r.dtCreation).FirstOrDefault();
            }
        }

        public static Entity.Receipt GetLastBy(bool isWinner, string productType)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Where(r => r.Product.type == productType).OrderByDescending(r => r.dtCreation).FirstOrDefault();
            }
        }

        public static int GetCountBy(int idPerson, string productType)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Where(r => r.idPerson == idPerson && r.Product.type == productType).Count();
            }
        }

        public static int GetCountBy(string productType, bool isWinner)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Where(r => r.isValidated == isWinner && r.Product.type == productType).Count();
            }
        }

        public static int GetCountByParticipation(string productType)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.Receipts.Where(r => r.Product.type == productType).Count();
            }
        }

        public static int GetCountBy(DateTime dtSince, string productType = null, DateTime? dtUntil = null)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.Receipts.Where(r => r.dtCreation >= dtSince).AsQueryable();

                if (productType != null)
                {
                    query = query.Where(r => r.Product.type == productType);
                }

                if (dtUntil != null)
                {
                    query = query.Where(r => r.dtCreation <= dtUntil);
                }

                return query.Count();
            }
        }

        public static int GetCountByAll(string productType = null)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var query = context.Receipts.Where(r => r.Product.type == productType).ToList();
                return query.Count();
            }
        }

        public static void declaredWinner(Entity.Receipt receipt)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var entity = context.Receipts.Where(r => r.idReceipt == receipt.idReceipt).FirstOrDefault();
                entity.isValidated = true;
                context.SaveChanges();
            }
        }

        public static void Save(Entity.Receipt receipt, bool direct)
        {
            ShiftIncRepository.Save<Entity.Receipt>(receipt);
        }

        private static object _receiptSaveLock = new object();
        public static ReceiptSaveResult Save(Entity.Receipt receipt)
        {
            var result = new ReceiptSaveResult()
            {
                isSuccess = false
            };

            Entity.Person tmpPerson = null;

            if (receipt.Person != null)
            {
                tmpPerson = receipt.Person;
            }
            receipt.Person = null;

            try
            {
                lock (_receiptSaveLock)
                {                
                    List<Entity.LuckyCode> luckyCodeRandom = new List<Entity.LuckyCode>();

                    if (IsAllowedToSave(receipt.idPerson, receipt.idProduct, "1"))
                    {
                        Entity.LuckyCode luckyCode = new Entity.LuckyCode();

                        
                        receipt.Person = null;
                        receipt.dtCreation = DateTime.Now;

                        ShiftIncRepository.Save<Entity.Receipt>(receipt);
                        receipt.Person = tmpPerson;
                        ApplyProductPromotion(receipt);

                        result.isSuccess = true;                      
                        
                    }
                }
            }
            catch (AnotherReceiptFoundTodayException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_another_receipt_today";
            }
            catch (ReceiptCountPerPersonExceededException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_excedeed_maximum_receipt_per_person";
            }
            catch (VendorNotFoundException)
            {
                result.isSuccess = false;
                result.ErrorCode = "error_vendor_not_allowed";
            }

            receipt.Person = tmpPerson;
            return result;
        }

        public static void SetValidated(int idReceipt, bool isValidated, string invalidateDescription = null)
        {
            var receipt = ShiftIncRepository.GetById<Entity.Receipt>(idReceipt);

            if (receipt != null)
            {
                receipt.isValidated = isValidated;
                receipt.invalidateDescription = invalidateDescription;

                ShiftIncRepository.Save<Entity.Receipt>(receipt);
            }

            if (receipt.Product.type == "intimus")
            {
                if (receipt.isValidated == true)
                {
                    ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.SetToSend(receipt.Person.email, ShellTanqueCheio.Business.NewsSending.NewsType.VPowerWinnerBullet, receipt.idReceipt);
                }
                else
                {
                    int idDescription = Business.InvalidateDescription.GetIdByDescription(receipt.invalidateDescription);
                    var NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomIlegivel;
                    switch (idDescription)
                    {
                        case 1:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomIlegivel;
                            break;
                        case 2:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.DataInvalida;
                            break;
                        case 3:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.ImagemNaoECupom;
                            break;
                        case 4:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomIncompleto;
                            break;
                        case 5:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomDiferenteProduto;
                            break;
                        case 6:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomJaCadastrado;
                            break;
                        case 7:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomSemPreenchimento;
                            break;
                        case 8:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomValorAbaixo;
                            break;
                        case 9:
                            NewsType = ShellTanqueCheio.Business.NewsSending.NewsType.CupomProdutoNaoParticipante;
                            break;
                    }
                    ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.SetToSend(receipt.Person.email, NewsType, receipt.idReceipt);
                }
            }
        }
        public static List<Entity.LuckyCode> GetWinnerMaiorLubrificantes(int numberCode)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.LuckyCodes.Include(lc => lc.Receipt).Include(lc => lc.Receipt.Person).Where(lc => lc.code >= numberCode).OrderBy(lc => lc.code).Take(10).ToList();
            }
        }

        public static List<Entity.LuckyCode> GetWinnerMenorLubrificantes(int numberCode)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.LuckyCodes.Include(lc => lc.Receipt).Include(lc => lc.Receipt.Person).Where(lc => lc.code <= numberCode).OrderByDescending(lc => lc.code).Take(10).ToList();
            }
        }

        private static void ApplyProductPromotion(Entity.Receipt receipt)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                var product = ShiftIncRepository.GetById<Entity.Product>(receipt.idProduct);

                if (product.type == "intimus")
                {
                    receipt.LuckyCodes = null;
                    var lastWinnerReceipt = GetLastBy(true, product.type);
                    int notWinnerCount = 0;
                    if (lastWinnerReceipt == null)
                    {
                        notWinnerCount = GetCountByAll(product.type);
                    }
                    else
                    {
                        notWinnerCount = GetCountBy(lastWinnerReceipt.dtCreation, product.type);
                    }

                    if (notWinnerCount >= 140)
                    {
                        receipt.isValidated = true;
                        Business.Receipt.declaredWinner(receipt);
                        //Disparo vencedor
                        ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.SetToSend(receipt.Person.email, ShellTanqueCheio.Business.NewsSending.NewsType.VPowerWinner, receipt.idReceipt);
                    }
                    else
                    {
                        ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.SetToSend(receipt.Person.email, ShellTanqueCheio.Business.NewsSending.NewsType.VPowerNotWinner, receipt.idReceipt);
                    }
                }
            }
        }
        public static int LuckyCodeRandom()
        {           
            Random rnd = new Random();
            int luckyCode = rnd.Next(0, 99999);
            return luckyCode;
        }

        public class ReceiptSaveResult
        {
            public bool isSuccess { get; set; }
            public string ErrorCode { get; set; }
        }
    }
}
