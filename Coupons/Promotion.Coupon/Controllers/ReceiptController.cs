using System;
using System.Web.Mvc;
using Promotion.Coupon.Entity.Extensions;
using System.Configuration;
using System.IO;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Application.Applications;

namespace Promotion.Coupon.Controllers
{
    [RoutePrefix("receipt")]
    public class ReceiptController : Controller
    {
        // GET: Receipt
        private readonly IReceiptApplication _receiptApplication;
        public ReceiptController()
        {
            _receiptApplication = new ReceiptApplication();
        }

        [HttpGet]
        [Route("file")]
        public ActionResult GetFile(string id)
        {
            id = id.Base64Decode();
            var receipt = _receiptApplication.GetById(Convert.ToInt32(id));
            ViewBag.file = receipt.imgBase64;

            return View("GetFile");
        }

        [HttpGet]
        [Route("file/{id}")]
        public ActionResult GetFileRaw(string id)
        {
            id = id.Base64Decode();
            var receipt = _receiptApplication.GetById(Convert.ToInt32(id));
            return File(receipt.imgBase64, "image/jpg");
        }
    }
}