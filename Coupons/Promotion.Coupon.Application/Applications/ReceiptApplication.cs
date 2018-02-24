using System;
using System.Linq;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Poco;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Exceptions;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Repository.Repositories;
using Promotion.Coupon.Application.Applications.Base;

namespace Promotion.Coupon.Application.Applications
{
    public class ReceiptApplication : ApplicationBase<Receipt>, IReceiptApplication
    {
        private readonly IReceiptRepository _receiptRepository;
        //private readonly IProductRepository _productRepository;
        private static object _receiptSaveLock = new object();

        public ReceiptApplication()
        {
            _receiptRepository = new ReceiptRepository();
            //_productRepository = new ProductRepository();
        }

        public Dictionary<string, int> GetCountPerDateBy(string productType, DateTime? @from = null, DateTime? to = null)
        {
            if (to == null)
                to = DateTime.Now;
            
            if (from == null)
                from = DateTime.Now.AddDays(-9);
           
            
            var response = _receiptRepository.GetCountPerDateBy(productType, from, to);

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

        public Dictionary<string, int> GetCountPerDateBy2(string productType, DateTime? @from = null, DateTime? to = null)
        {
            from = DateTime.Now.AddDays(-2);
            if (to == null)
            {
                to = DateTime.Now;
            }
            

            var response = _receiptRepository.GetCountPerDateBy2(productType, from, to);

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

        public bool IsAllowedToSave(int idPerson, int idProduct, string cnpj)
        {
            //var product = _productRepository.Get(idProduct);

            //var lastReceipt = GetLastBy(idPerson, product.type);
            //bool isLastReceiptNotToday = lastReceipt == null || (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime(lastReceipt.dtCreation.Year, lastReceipt.dtCreation.Month, lastReceipt.dtCreation.Day));
            //if (!isLastReceiptNotToday)
            //    throw new AnotherReceiptFoundTodayException();


            //var countReceipts = GetCountBy(idPerson, product.type);
            //bool isReceiptCountUnderLimit = countReceipts < (product.type == "v-power" ? 30 : 5);
            //if (!isReceiptCountUnderLimit)
            //    throw new ReceiptCountPerPersonExceededException();

            return true;
        }

        public List<Receipt> GetReceiptsByIdPerson(int idPerson)
        {
            return _receiptRepository.GetReceiptsByIdPerson(idPerson);
        }

        public List<Receipt> GetReceiptsBy(DateTime @from, DateTime to)
        {
            return _receiptRepository.GetReceiptsBy(from, to);
        }

        public List<ViewReceiptExport> GetReceiptsBy2(DateTime @from, DateTime to)
        {
            return _receiptRepository.GetReceiptsBy2(from, to);
        }

        public int GetCountByNotWinners()
        {
            return _receiptRepository.GetCountByNotWinners();
        }

        public List<Receipt> GetReceiptsByWinners(string type)
        {
            return _receiptRepository.GetReceiptsByWinners(type);
        }

        public List<Receipt> GetBy(string type, bool isWinner, bool? isValidated)
        {
            return _receiptRepository.GetBy(type,isWinner,isValidated);
        }

        public Receipt GetLastBy(int idPerson, string productType)
        {
            return _receiptRepository.GetLastBy(idPerson, productType);
        }

        public Receipt GetById(int idReceipt)
        {
            return _receiptRepository.GetById(idReceipt);
        }

        public Receipt GetLastBy(bool isWinner, string productType)
        {
            return _receiptRepository.GetLastBy(isWinner, productType);
        }

        public int GetCountBy(int idPerson, string productType)
        {
            return _receiptRepository.GetCountBy(idPerson, productType);
        }

        public int GetCountBy(string productType, bool isWinner)
        {
            return _receiptRepository.GetCountBy(productType, isWinner);
        }

        public int GetCountByParticipation(string productType)
        {
            return _receiptRepository.GetCountByParticipation(productType);
        }

        public int GetCountBy(DateTime dtSince, string productType = null, DateTime? dtUntil = null)
        {

            return _receiptRepository.GetCountBy(dtSince, productType,dtUntil);
        }

        public int GetCountByAll(string productType = null)
        {
            return _receiptRepository.GetCountByAll(productType);
        }

        public void DeclaredWinner(Receipt receipt)
        {
            _receiptRepository.DeclaredWinner(receipt);
        }

        public void Save(Receipt receipt, bool direct)
        {
            this.Insert(receipt);
        }

        public ReceiptSaveResult Save(Receipt receipt)
        {
            var result = new ReceiptSaveResult()
            {
                isSuccess = false
            };

            Person tmpPerson = null;

            if (receipt.Person != null)
            {
                tmpPerson = receipt.Person;
            }
            receipt.Person = null;

            try
            {
                lock (_receiptSaveLock)
                {
                    if (IsAllowedToSave(receipt.idPerson, receipt.idProduct, "1"))
                    {
                        receipt.Person = null;
                        receipt.dtCreation = DateTime.Now;
                        new ReceiptRepository().Insert(receipt);
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

        public void SetValidated(int idReceipt, bool isValidated, string invalidateDescription = null)
        {
            var receipt = new ReceiptRepository().Get(idReceipt);

            if (receipt != null)
            {
                receipt.isValidated = isValidated;
                //receipt.invalidateDescription = invalidateDescription;
                new ReceiptRepository().Insert(receipt);
            }

            if (receipt.Product.type == "intimus")
            {
                if (receipt.isValidated == true)
                {
                    
            //        new NewsSendingRepository().SetToSend(receipt.Person.email, ENewsType.VPowerWinnerBullet, receipt.idReceipt);
            //    }
            //    else
            //    {
            //        //int idDescription = InvalidateDescription.GetIdByDescription(receipt.invalidateDescription);
            //        var newsType = ENewsType.CupomIlegivel;
            //        switch (idDescription)
            //        {
            //            case 1:
            //                newsType = ENewsType.CupomIlegivel;
            //                break;
            //            case 2:
            //                newsType = ENewsType.DataInvalida;
            //                break;
            //            case 3:
            //                newsType = ENewsType.ImagemNaoECupom;
            //                break;
            //            case 4:
            //                newsType = ENewsType.CupomIncompleto;
            //                break;
            //            case 5:
            //                newsType = ENewsType.CupomDiferenteProduto;
            //                break;
            //            case 6:
            //                newsType = ENewsType.CupomJaCadastrado;
            //                break;
            //            case 7:
            //                newsType = ENewsType.CupomSemPreenchimento;
            //                break;
            //            case 8:
            //                newsType = ENewsType.CupomValorAbaixo;
            //                break;
            //            case 9:
            //                newsType = ENewsType.CupomProdutoNaoParticipante;
            //                break;
            //        }

            //        new NewsSendingRepository().SetToSend(receipt.Person.email, newsType, receipt.idReceipt);
            //    }
            //}
        }

        public void ApplyProductPromotion(Receipt receipt)
        {
                //var product = new ProductRepository().Get(receipt.idProduct);

                //if (product.type == "intimus")
                //{
                //    receipt.LuckyCode = null;
                //    var lastWinnerReceipt = GetLastBy(true, product.type);
                //    int notWinnerCount = 0;
                //    notWinnerCount = lastWinnerReceipt == null ? GetCountByAll(product.type) : GetCountBy(lastWinnerReceipt.dtCreation, product.type);

                //    if (notWinnerCount >= 140)
                //    {
                //        receipt.isValidated = true;

                //        new ReceiptRepository().DeclaredWinner(receipt);
                //    //Disparo vencedor

                //        new NewsSendingRepository().SetToSend(receipt.Person.email, ENewsType.VPowerWinner, receipt.idReceipt);
                //    }
                //    else
                //    {
                //        new NewsSendingRepository().SetToSend(receipt.Person.email, ENewsType.VPowerNotWinner, receipt.idReceipt);
                //    }
                //}
            
        }

        public int LuckyCodeRandom()
        {
            Random rnd = new Random();
            int luckyCode = rnd.Next(0, 99999);
            return luckyCode;
        }
    }
}