using System;
using System.Web.Mvc;
using Promotion.Coupon.Entity.Extensions;

namespace Promotion.Coupon.Controllers
{
    [RoutePrefix("receipt")]
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("file")]
        public ActionResult GetFile(string id)
        {
            ViewBag.file = "/receipt/file/" + id;

            return View("GetFile");
        }

        [HttpGet]
        [Route("file/{id}")]
        public ActionResult GetFileRaw(string id)
        {
            id = id.Base64Decode();

            return File("/ReceiptFiles/" + Convert.ToInt32(id).ToString() + ".jpg", "image/jpg");
        }
    }
}