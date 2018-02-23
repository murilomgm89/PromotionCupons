using System;
using System.Web.Mvc;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Models;

namespace Promotion.Coupon.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonApplication _personApplication;
        private readonly INewsSendingApplication _newsSendingApplication;

        public HomeController()
        {
            _personApplication = new PersonApplication();
            _newsSendingApplication = new NewsSendingApplication();
        }
        // GET: Home
        public ActionResult Index()
        {


            return Redirect("/Admin/Login");
        }

        public ActionResult InitFlow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostInitFlow(ReceiptViewModel vm)
        {
            var data = new Person()
            {
                cpf = vm.cpf,
                email = vm.email,
                dtCreation = DateTime.Now
            };
            //Inserir ou salvar imagem na pasta de imagens
            vm.ReceiptFile.SaveAs($"~/ReceiptFiles/cupom_{vm.email}_{data.dtCreation}");
            
            /*
             * Retornos:
             * Salvo com sucesso
             * Se o CPF já foi considerado um ganhador ( Devemos ter um retorno de CPF já ganhador ) 
             */

            //Pos cadastro
            //Após o cadastro o cliente deve receber um e - mail infromando da sua participação
            //O cadastro deve aparecer no CMS como cadastro pendente de validação

            //Insere
            _personApplication.Insert(data);
            //Envia email
            _newsSendingApplication.SetToSend(data.email, ENewsType.CreatePersonVPower, null, data.idPerson);

            return View();
        }
    }
}