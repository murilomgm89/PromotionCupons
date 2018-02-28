using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Promotion.Coupon.Models;
using System.Collections.Generic;
using System.Net;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Exceptions;
using Promotion.Coupon.Entity.Extensions;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Entity.Handle;

namespace Promotion.Coupon.Controllers
{
    [RoutePrefix("promotion")]
    public class PromotionController : Controller
    {
        private readonly IPersonApplication _personApplication;
        private readonly IReceiptApplication _receiptApplication;
        private readonly INewsSendingApplication _newsSendingApplication;
        private readonly IProductApplication _productApplication;
        private readonly IBlockedCpfApplication _blockedCPFRepository;
        public PromotionController()
        {
            _personApplication = new PersonApplication();
            _receiptApplication = new ReceiptApplication();
            _newsSendingApplication = new NewsSendingApplication();
            _blockedCPFRepository = new BlockedCPFApplication();
        }
        // GET: Promotion
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("product-{productType}")]
        public ActionResult PromotionIndex(string productType)
        {
            //DateTime dataStart = new DateTime(2016, 7, 1);
            //if (DateTime.Now < dataStart)
            //{
            //    return View("NotStartPromotion");
            //}            

            Session["ProductType"] = productType;
            ViewBag.ProductType = productType;

            return View("PromotionIndex");
        }
        
        [HttpPost]
        [Route("receipt/save")]
        public String SaveReceipt(ReceiptViewModel model)
        {
            
            Person person;
            Person entity;
            ReceiptSaveViewModel result = new ReceiptSaveViewModel();           
            try
            {
                if (model.cpf != null)
                    model.cpf = model.cpf.Replace(".", "").Replace("-", "");
                if (_blockedCPFRepository.GetCPF(long.Parse(model.cpf)) != null)
                {
                    return "error_blockedCPF";
                }
                
                person = _personApplication.GetByCpf(model.cpf);
                if (person != null)
                {
                    //Valida se existe cpf que ja ganhou
                    if (_receiptApplication.GetReceiptWinnerByIdPerson(person.idPerson) != null)
                    {
                        return "error_participation_computed";
                    }                   
                }

                if (model.cpf != null)
                {                   
                    if (person == null)
                    {
                        entity = model.Parse();
                        _personApplication.Insert(entity);
                        person = _personApplication.GetByCpf(model.cpf);
                    }
                    model.Receipt.idPerson = person.idPerson;
                    model.Receipt.Person = person;
                }
                
                var validImageTypes = new string[]
                {
                "image/gif",
                "image/jpg",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
                };

                if (model.ReceiptFile == null || model.ReceiptFile.ContentLength == 0 || !validImageTypes.Contains(model.ReceiptFile.ContentType))
                {                   
                    return "error_upload_not_valid";
                }

                model.Receipt.isValidated = null;

                var saveResult = _receiptApplication.Save(model.Receipt);

                if (saveResult.isSuccess)
                {
                    var uploadDir = "~/ReceiptFiles";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.Receipt.idReceipt + ".jpg");
                    model.ReceiptFile.SaveAs(imagePath);

                    result.Status = "success";
                    result.Receipt = model.Receipt.ApiSerialize();

                    result.Receipt = model.Receipt;

                    result.Receipts = _receiptApplication.GetReceiptsByIdPerson(model.Receipt.idPerson);
                   
                    var @from = "Promoção Gympass Imtimus Sport <promocaogympass@intimus.com.br>";
                    var subject = "Obrigado por participar, aguarde a válidação de seu cupom!";

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/News/NewsVoucherView.cshtml"));
                    EmailHandle.SendEmail(@from, model.email, subject, content);

                    return "sucesso";
                }
                else
                {                    
                    return "error";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
            return "error";
        }
    }
}