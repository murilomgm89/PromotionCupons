using AttributeRouting.Web.Mvc;
using ShiftInc.Raizen.ShellTanqueCheio.Business.Exceptions;
using ShiftInc.Raizen.ShellTanqueCheio.Entity;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Controllers
{
    public class ReceiptController : Controller
    {
        [GET("/promotion/product-{productType}")]
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

        [GET("/promotion/EndPromotion")]
        public ActionResult PromotionEnd(string cpf)
        {
            var person = Business.Person.GetByCPFNotBlackList(cpf);
            EndPromiton model = new EndPromiton();
            model.Receipts = new List<Receipt>();
            if (person != null)
            {
                model.Receipts = Business.Receipt.GetReceiptsByIdPerson(person.idPerson);
            }
            return View("ReceiptEndPromotion", model);
        }


        [POST("/promotion/cpf-check")]
        public ActionResult GetPersonByCPF(string cpf, string productType)
        {
            try
            {
                var person = Business.Person.GetByCPF(cpf);
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
                    HttpCookie aCookie = new HttpCookie("person");
                    aCookie.Value = cpf;
                    Response.Cookies.Add(aCookie);
                    HttpCookie Cookie = new HttpCookie("productType");
                    Cookie.Value = productType;
                    Response.Cookies.Add(Cookie);
                    ReceiptViewModel model = new ReceiptViewModel();
                    model.productType = productType;
                    model.cpf = person.cpf;
                    model.person = person;
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
            catch (PersonCPFFoundInBlacklistException ex)
            {
                ViewBag.Error = "error_cpf_found_in_blacklist";
                return PromotionIndex(productType);
            }
        }

        [GET("/promotion/personal-data")]
        public ActionResult PersonalDataForm(string cpf, string productType)
        {
            var sessionPerson = (Entity.Person)Session["Entity.Person"];

            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);

            var person = new Entity.Person();            

            if (sessionPerson != null)
            {
                person = Business.Person.GetByCPF(sessionPerson.cpf);
                if (person == null)
                {
                    person = new Entity.Person();                  
                }
                person.idPerson = sessionPerson.idPerson;
                person.cpf = sessionPerson.cpf;               
            }
            else
            {
                person = Business.Person.GetByCPF(cpf);
                if (person != null)
                {
                    person.idPerson = person.idPerson;
                    person.cpf = cpf;                    
                }
                else {
                    person = new Entity.Person();                   
                    person.cpf = cpf;
                }
            }
            ReceiptViewModel model = new ReceiptViewModel();
            model.productType = productType;
            model.person = person;
            model.cpf = person.cpf;
            return View("PersonalDataForm", model);
        }

        [POST("promotion/person/save")]
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

            var result = Business.Person.Save(model.person);

            if (result.isSuccess)
            {
                Session["Entity.Person"] = model.person;
                var productType = "";
                if(Session["ProductType"] != null){
                    productType = Session["ProductType"].ToString();
                }               
                if (Session["ProductType"] == null)
                {
                    productType = model.productType;
                }              
                var newsType = ShellTanqueCheio.Business.NewsSending.NewsType.CreatePersonLubrificante;
                if (productType == "v-power")
                {
                    newsType = ShellTanqueCheio.Business.NewsSending.NewsType.CreatePersonVPower;
                }
                ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.SetToSend(model.person.email, newsType, null, model.person.idPerson);
                ReceiptViewModel modelR = new ReceiptViewModel();
                modelR.productType = model.productType;
                modelR.person = model.person;
                return ReceiptDataForm(modelR);
            }
            else
            {
                ViewBag.Error = result.ErrorCode;
                return View("PersonalDataForm");
            }
        }

        [GET("/promotion/receipt-data")]
        public ActionResult ReceiptDataForm(ReceiptViewModel receipt)
        {
            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);
            var session = Session["Entity.Person"];
            if (session != null)
            {
                var person = (Entity.Person)Session["Entity.Person"];
                receipt.Receipt = new Receipt()
                {
                    idPerson = person.idPerson,
                    Person = person
                };
                receipt.Receipts = Business.Receipt.GetReceiptsByIdPerson(person.idPerson);
            }
            else
            {
                if(receipt.cpf != null){
                    var person = Business.Person.GetByCPF(receipt.cpf);
                    receipt.Receipt = new Receipt()
                    {
                        idPerson = person.idPerson,
                        Person = person
                    };
                    receipt.Receipts = Business.Receipt.GetReceiptsByIdPerson(person.idPerson);
                }
            }

            receipt.ProductList = Business.Product.GetByType(receipt.productType == "v-power" ? "v-power" : "lubrificantes").ToList();
            return View("ReceiptDataForm", receipt);
        }

        [GET("/promotion/winners/{type}")]
        public ActionResult Winners(string type)
        {
            ReceiptSaveViewModel winnersViewModel = new ReceiptSaveViewModel();
            //winnersViewModel.Receipts = Business.Receipt.GetReceiptsByWinners(type);
            return View("Winners_" + type, winnersViewModel);

        }       

        [POST("/promotion/receipt/save")]
        public ActionResult SaveReceipt(ReceiptViewModel model)
        {
            //força fim campanha
            //EndPromiton modelEnd = new EndPromiton();
            //modelEnd.Receipts = new List<Receipt>();
            //return View("ReceiptEndPromotion", modelEnd);

            ReceiptSaveViewModel result = new ReceiptSaveViewModel();
            if (Session["Entity.Person"] != null)
            {
                var person = (Entity.Person)Session["Entity.Person"];
                model.Receipt.idPerson = person.idPerson;
                model.Receipt.Person = person;
            }
            else
            {
                if (model.cpf != null)
                {
                    var person = Business.Person.GetByCPF(model.cpf);
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
            
            var saveResult = Business.Receipt.Save(model.Receipt);

            if (saveResult.isSuccess)
            {
                var uploadDir = "~/ReceiptFiles";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), model.Receipt.idReceipt + ".jpg");
                model.ReceiptFile.SaveAs(imagePath);

                result.Status = "success";
                result.Receipt = model.Receipt.ApiSerialize();

                model.Receipt.LuckyCodes = model.Receipt.LuckyCodes == null ? null : model.Receipt.LuckyCodes.Select(lc => lc.ApiSerialize()).ToList();
                result.Receipt = model.Receipt;

                result.Receipts = Business.Receipt.GetReceiptsByIdPerson(model.Receipt.idPerson);

                if (result.Receipt.LuckyCodes == null || result.Receipt.LuckyCodes.Count() == 0) // v-power 
                {                   
                    return View("ReceiptFinalWinner", result);
                }
                else // lubrificantes
                {
                    return View("ReceiptFinalShowLuckyCode", result);
                }
            }
            else
            {
                model.Status = "error";
                model.ErrorCode = saveResult.ErrorCode;

                return ReceiptDataForm(model);
            }
        }

        [GET("/receipt/file")]
        public ActionResult GetFile(string id)
        {
            ViewBag.file = "/receipt/file/" + id;

            return View("GetFile");
        }

        [GET("/receipt/file/{id}")]
        public ActionResult GetFileRaw(string id)
        {
            id = id.Base64Decode();

            return File("/ReceiptFiles/" + Convert.ToInt32(id).ToString() + ".jpg", "image/jpg");
        }

        [GET("/erro")]
        public ActionResult Error()
        {
            return View("erro");
        }
    }
}