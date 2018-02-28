using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Areas.Admin.Models;
using Promotion.Coupon.Filters;

namespace Promotion.Coupon.Areas.Admin.Controllers
{
    [AdminAccessFilter]
    public class ReportController : Controller
    {

        private readonly IReceiptApplication _receiptApplication;
        private readonly ILuckyCodeApplication _luckyCodeApplication;
        private readonly IPersonApplication _personApplication;

        public ReportController()
        {
            _receiptApplication = new ReceiptApplication();           
            _personApplication = new PersonApplication();

        }

        [GET("/admin/report/participations")]
        public ActionResult Participations()
        {
            var model = new PartitipationsReportViewModel();

            model.from = DateTime.Now.AddDays(-2);
            model.to = DateTime.Now;

            if (Request.QueryString["Participations.From"] != null)
            {
                model.from = fromString(Request.QueryString["Participations.From"].ToString());
                Session["Participations.From"] = model.from;
            }

            if (Request.QueryString["Participations.To"] != null)
            {
                model.to = fromString(Request.QueryString["Participations.To"].ToString());
                Session["Participations.To"] = model.to;
            }

            model.ReceiptsChartData = _receiptApplication.GetCountPerDateBy(null, model.from, model.to.AddDays(1)).Select(d => new DashboardViewModel.ChartItem() { Label = d.Key, Value = d.Value }).OrderBy(d => d.Label).ToList();
                model.ReceiptCount = _receiptApplication.GetCountBy(model.from, null, model.to.AddDays(1));           
            return View("~/Areas/Admin/Views/Report/Participations.cshtml", model);
        }

        private DateTime fromString(string dt)
        {
            var split = dt.Split('/');

            return Convert.ToDateTime(split[2] + "-" + split[1] + "-" + split[0]);
        }

        [GET("/admin/report/lucky-codes")]
        public ActionResult LuckyCodes()
        {
            var model = new LuckyCodeReportViewModel();

            model.from = DateTime.Now.AddDays(-2);
            model.to = DateTime.Now;

            if (Request.QueryString["Participations.From"] != null)
            {
                model.from = fromString(Request.QueryString["Participations.From"].ToString());
                Session["Participations.From"] = model.from;
            }

            if (Request.QueryString["Participations.To"] != null)
            {
                model.to = fromString(Request.QueryString["Participations.To"].ToString());
                Session["Participations.To"] = model.to;
            }
            model.to = model.to.AddDays(1);
            model.to = model.to.AddDays(-1);

            return View("~/Areas/Admin/Views/Report/LuckyCodes.cshtml", model);
        }

        [GET("/admin/report/cpf-export")]
        public ActionResult CPFExport()
        {
            DateTime from = DateTime.Now.AddDays(-2);
            DateTime to = DateTime.Now;

            if (Session["Participations.From"] != null)
            {
                from = (DateTime)Session["Participations.From"];
            }

            if (Session["Participations.To"] != null)
            {
                to = (DateTime)Session["Participations.To"];
            }

            var people = _personApplication.GetBy(from, to.AddDays(1));

            var sbResult = new StringBuilder();
            sbResult.Append("Nome;CPF;Email;Data de Cadastro\n");

            foreach (var p in people)
            {
                sbResult.Append(p.name);
                sbResult.Append(";");
                sbResult.Append(p.cpf);
                sbResult.Append(";");
                sbResult.Append(p.email);
                sbResult.Append(";");
                sbResult.Append(p.dtCreation.ToString("dd/MM/yyyy"));
                sbResult.Append(";");
                sbResult.Append("\n");
            }

            return File(new System.Text.UnicodeEncoding().GetBytes(sbResult.ToString()), "text/csv", "Exportacao_DadosCadastrais_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ".csv");
        }

        [GET("/admin/report/luckycodes-export")]
        public ActionResult LuckyCodesExport()
        {
            DateTime from = DateTime.Now.AddDays(-2);
            DateTime to = DateTime.Now;

            if (Session["Participations.From"] != null)
            {
                from = (DateTime)Session["Participations.From"];
            }

            if (Session["Participations.To"] != null)
            {
                to = (DateTime)Session["Participations.To"];
            }

            //var codes = _luckyCodeApplication.GetBy(from, to.AddDays(1));

            var sbResult = new StringBuilder();
            sbResult.Append("Numero da Sorte; Data de Cadastro; Data de Sorteio\n");

            //foreach (var p in codes)
            //{
            //    sbResult.Append(p.code);
            //    sbResult.Append(";");
            //    sbResult.Append(p.Receipt.dtCreation.ToString("dd/MM/yyyy HH:mm"));
            //    sbResult.Append(";");
            //    sbResult.Append(p.dtRaffle.Value.ToString("dd/MM/yyyy HH:mm"));
            //    sbResult.Append(";");
            //    sbResult.Append("\n");
            //}

            return File(new System.Text.UnicodeEncoding().GetBytes(sbResult.ToString()), "text/csv", "Exportacao_NumerosSorte_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ".csv");
        }

        [GET("/admin/report/receipt-export")]
        public ActionResult ReceiptExport()
        {
            DateTime from = DateTime.Now.AddDays(-2);
            DateTime to = DateTime.Now;

            if (Session["Participations.From"] != null)
            {
                from = (DateTime)Session["Participations.From"];
            }

            if (Session["Participations.To"] != null)
            {
                to = (DateTime)Session["Participations.To"];
            }

            var receipts = _receiptApplication.GetReceiptsBy2(from, to.AddDays(1));


            var sbResult = new StringBuilder();
            sbResult.Append("Premiado; Voucher; Data do Cadastro do Recibo; Nome do Participante;CPF;Email;Data de Cadastro do Participante;\n");

            foreach (var r in receipts)
            {
                sbResult.Append(r.Validado == true ? "SIM" : (r.Validado == false ? "NAO" : "Aprovação Pendente"));
                sbResult.Append(";");
                sbResult.Append(r.VoucherVinculado == null ? "" : r.VoucherVinculado.ToString());
                sbResult.Append(";");     
                sbResult.Append(r.Data_do_Cadastro_do_Recibo.ToString("dd/MM/yyyy HH:mm"));
                sbResult.Append(";");
                sbResult.Append(r.Nome_do_Participante);
                sbResult.Append(";");
                sbResult.Append(r.cpf);
                sbResult.Append(";");
                sbResult.Append(r.email);
                sbResult.Append(";");
                sbResult.Append(r.Data_de_Cadastro_do_Participante.ToString("dd/MM/yyyy"));
                sbResult.Append(";");
                sbResult.Append("\n");
            }

            return File(new System.Text.UnicodeEncoding().GetBytes(sbResult.ToString()), "text/csv", "Exportacao_Cupons_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ".csv");
        }
    }
}