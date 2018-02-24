using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Promotion.Coupon.Models;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Exceptions;
using Promotion.Coupon.Entity.Extensions;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Application.Applications;

namespace Promotion.Coupon.Controllers
{
    [RoutePrefix("promotion")]
    public class PromotionController : Controller
    {
        private readonly IPersonApplication _personApplication;
        private readonly IReceiptApplication _receiptApplication;
        private readonly INewsSendingApplication _newsSendingApplication;
        private readonly IProductApplication _productApplication;
        public PromotionController()
        {
            _personApplication = new PersonApplication();
            _receiptApplication = new ReceiptApplication();
            _newsSendingApplication = new NewsSendingApplication();
            //_productApplication = new ProductApplication();
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

        [HttpGet]
        [Route("EndPromotion")]
        public ActionResult PromotionEnd(string cpf)
        {
            var person = _personApplication.GetByCpfNotBlackList(cpf);
            var model = new EndPromotionViewModel
            {
                Receipts = new List<Receipt>()
            };

            if (person != null)
            {
                model.Receipts = _receiptApplication.GetReceiptsByIdPerson(person.idPerson);
            }
            return View("ReceiptEndPromotion", model);
        }

        [HttpPost]
        [Route("cpf-check")]
        public ActionResult GetPersonByCPF(string cpf, string productType)
        {
            try
            {
                var person = _personApplication.GetByCpf(cpf);
                Session["ProductType"] = productType;

                DateTime dataStart = new DateTime(2019, 10, 3);
                if (DateTime.Now > dataStart)
                {
                    if (person != null)
                    {
                        return PromotionEnd(cpf);
                    }
                    else
                    {
                        ViewBag.Error = "error_end_promotion";
                        return PromotionIndex(productType);
                    }
                }


                if (person != null)
                {
                    Session["Entity.Person"] = person;
                    HttpCookie aCookie = new HttpCookie("person")
                    {
                        Value = cpf
                    };
                    Response.Cookies.Add(aCookie);
                    HttpCookie cookie = new HttpCookie("productType");
                    cookie.Value = productType;
                    Response.Cookies.Add(cookie);
                    ReceiptViewModel model = new ReceiptViewModel
                    {
                        productType = productType,
                        cpf = person.cpf,
                        person = person
                    };

                    //Promoção finalizada
                    return View("PersonalDataConfirmation", model);
                }

                Session["Entity.Person"] = new Person()
                {
                    cpf = cpf
                };

                ViewBag.ProductType = productType;
                //Promoção finalizada
                return PersonalDataForm(cpf, productType);
            }
            catch (PersonCpfFoundInBlacklistException ex)
            {
                ViewBag.Error = "error_cpf_found_in_blacklist";
                return PromotionIndex(productType);
            }
        }

        [HttpGet]
        [Route("personal-data")]
        public ActionResult PersonalDataForm(string cpf, string productType)
        {
            var sessionPerson = (Person)Session["Entity.Person"];

            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);

            var person = new Person();

            if (sessionPerson != null)
            {
                person = _personApplication.GetByCpf(sessionPerson.cpf);
                if (person == null)
                {
                    person = new Person();
                }
                person.idPerson = sessionPerson.idPerson;
                person.cpf = sessionPerson.cpf;
            }
            else
            {
                person = _personApplication.GetByCpf(cpf);
                if (person != null)
                {
                    person.idPerson = person.idPerson;
                    person.cpf = cpf;
                }
                else
                {
                    person = new Person();
                    person.cpf = cpf;
                }
            }
            ReceiptViewModel model = new ReceiptViewModel
            {
                productType = productType,
                person = person,
                cpf = person.cpf
            };

            return View("PersonalDataForm", model);
        }

        [HttpPost]
        [Route("person/save")]
        public ActionResult SavePerson(ReceiptViewModel model)
        {
            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "error_model_not_valid";
                return View("PersonalDataForm", model.person);
            }

            var result = _personApplication.Save(model.person);

            if (result.isSuccess)
            {
                Session["Entity.Person"] = model.person;
                var productType = "";
                if (Session["ProductType"] != null)
                {
                    productType = Session["ProductType"].ToString();
                }
                if (Session["ProductType"] == null)
                {
                    productType = model.productType;
                }
                var newsType = ENewsType.CreatePersonLubrificante;
                if (productType == "v-power")
                {
                    newsType = ENewsType.CreatePersonVPower;
                }
                _newsSendingApplication.SetToSend(model.person.email, newsType, null, model.person.idPerson);
                var modelR = new ReceiptViewModel
                {
                    productType = model.productType,
                    person = model.person
                };
                return ReceiptDataForm(modelR);
            }
            else
            {
                ViewBag.Error = result.ErrorCode;
                return View("PersonalDataForm");
            }
        }

        [HttpGet]
        [Route("receipt-data")]
        public ActionResult ReceiptDataForm(ReceiptViewModel receipt)
        {
            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);
            var session = Session["Entity.Person"];
            if (session != null)
            {
                var person = (Person)Session["Entity.Person"];
                receipt.Receipt = new Receipt()
                {
                    idPerson = person.idPerson,
                    Person = person
                };
                receipt.Receipts = _receiptApplication.GetReceiptsByIdPerson(person.idPerson);
            }
            else
            {
                if (receipt.cpf != null)
                {
                    var person = _personApplication.GetByCpf(receipt.cpf);
                    receipt.Receipt = new Receipt()
                    {
                        idPerson = person.idPerson,
                        Person = person
                    };
                    receipt.Receipts = _receiptApplication.GetReceiptsByIdPerson(person.idPerson);
                }
            }

            //receipt.ProductList = _productApplication.GetByType(receipt.productType == "v-power" ? "v-power" : "lubrificantes").ToList();
            return View("ReceiptDataForm", receipt);
        }

        [HttpGet]
        [Route("winners/{type}")]
        public ActionResult Winners(string type)
        {
            ReceiptSaveViewModel winnersViewModel = new ReceiptSaveViewModel();
            //winnersViewModel.Receipts = Business.Receipt.GetReceiptsByWinners(type);
            return View("Winners_" + type, winnersViewModel);

        }

        [HttpPost]
        [Route("receipt/save")]
        public ActionResult SaveReceipt(ReceiptViewModel model)
        {
            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);

            ReceiptSaveViewModel result = new ReceiptSaveViewModel();
            if (Session["Entity.Person"] != null)
            {
                var person = (Person)Session["Entity.Person"];
                model.Receipt.idPerson = person.idPerson;
                model.Receipt.Person = person;
            }
            else
            {
                if (model.cpf != null)
                {
                    var person = _personApplication.GetByCpf(model.cpf);
                    model.Receipt.idPerson = person.idPerson;
                    model.Receipt.Person = person;
                }
            }

            //if (!ModelState.IsValid)
            //{
            //    model.Status = "error";
            //    model.ErrorCode = "error_model_not_valid";

            //    return ReceiptDataForm(model);
            //}

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
                model.Status = "error";
                model.ErrorCode = "error_upload_not_valid";

                return ReceiptDataForm(model);
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

                //model.Receipt.LuckyCode = model.Receipt.LuckyCode == null ? null : model.Receipt.LuckyCode.Select(lc => lc.ApiSerialize()).ToList();
                result.Receipt = model.Receipt;

                result.Receipts = _receiptApplication.GetReceiptsByIdPerson(model.Receipt.idPerson);

                //if (result.Receipt.LuckyCode == null || !result.Receipt.LuckyCode.Any()) // v-power 
                //{
                //    return View("ReceiptFinalWinner", result);
                //}
                //else // lubrificantes
                //{
                //    return View("ReceiptFinalShowLuckyCode", result);
                //}
            }
            else
            {
                model.Status = "error";
                model.ErrorCode = saveResult.ErrorCode;

                return ReceiptDataForm(model);
            }

            return null;
        }

    }
}