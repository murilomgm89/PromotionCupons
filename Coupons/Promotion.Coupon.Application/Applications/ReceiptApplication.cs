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
using Promotion.Coupon.Entity.Handle;

namespace Promotion.Coupon.Application.Applications
{
    public class ReceiptApplication : ApplicationBase<Receipt>, IReceiptApplication
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly INewsSendingRepository _newsSendingRepository;
        private readonly IVoucherRepository _voucherRepository;

        //private readonly IProductRepository _productRepository;
        private static object _receiptSaveLock = new object();

        public ReceiptApplication()
        {
            _receiptRepository = new ReceiptRepository();
            _newsSendingRepository = new NewsSendingRepository();
            _voucherRepository = new VoucherRepository();
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
        public Receipt GetReceiptWinnerByIdPerson(int idPerson)
        {
            return _receiptRepository.GetReceiptWinnerByIdPerson(idPerson);
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
                    receipt.Person = null;
                    receipt.dtCreation = DateTime.Now;
                    _receiptRepository.Insert(receipt);
                    receipt.Person = tmpPerson;
                    ApplyProductPromotion(receipt);

                    result.isSuccess = true;
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

        public void SetValidated(int idReceipt, bool isValidated, string FileNews, string ambiente)
        {
            var receipt = _receiptRepository.GetById(idReceipt);

            if (receipt != null)
            {
                receipt.isValidated = isValidated;
                if (receipt.idReceipt.Equals(0))
                    _receiptRepository.Insert(receipt);
                else
                    _receiptRepository.Update(receipt);
            }

            if (receipt == null)
                return;

            if (receipt.isValidated == true)
            {
                var voucher = _voucherRepository.GenarateWinner(receipt.Person.idPerson);
                var @from = "Promoção Gympass Imtimus Sport <noreply@relay.kccweb.net>";
                var subject = "Voce ganhou um voucher de desconto Gympass, Obrigado por participar!.";
                string content = System.IO.File.ReadAllText(FileNews);
                content = content.Replace("{VOUCHER}", voucher.code.ToString());
                string[] name = receipt.Person.name.Split(' ');
                content = content.Replace("{NOME}", name[0].ToString());
                EmailHandle.SendEmail(@from, receipt.Person.email, subject, content, ambiente);
            }

            if (receipt.isValidated == false)
            {
                var @from = "Promoção Gympass Imtimus Sport <noreply@relay.kccweb.net>";
                var subject = "Olá, nós recebemos a sua solicitação porém não foi dessa vez, continue tentando.";
                string content = System.IO.File.ReadAllText(FileNews);
                string[] name = receipt.Person.name.Split(' ');
                content = content.Replace("{NOME}", name[0].ToString());                
                EmailHandle.SendEmail(@from, receipt.Person.email, subject, content, ambiente);
            }
        }
        public void ApplyProductPromotion(Receipt receipt)
        {
            //
                //receipt.LuckyCode = null;
                //var lastWinnerReceipt = GetLastBy(true, product.type);
                //int notWinnerCount = 0;
                //notWinnerCount = lastWinnerReceipt == null ? GetCountByAll("1") : GetCountBy(lastWinnerReceipt.dtCreation, product.type);

                //if (notWinnerCount >= 140)
                //{
                //    receipt.isValidated = true;

                //    _receiptRepository.DeclaredWinner(receipt);
                //    //Disparo vencedor

                //    _newsSendingRepository.SetToSend(receipt.Person.email, ENewsType.VPowerWinner, receipt.idReceipt);
                //}
                //else
                //{
                //    _newsSendingRepository.SetToSend(receipt.Person.email, ENewsType.VPowerNotWinner, receipt.idReceipt);
                //}
            //}

        }

        public int LuckyCodeRandom()
        {
            return _receiptRepository.LuckyCodeRandom();
           
        }
    }
}