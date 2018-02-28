using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Net;
using Promotion.Coupon.Application.Applications;
using Promotion.Coupon.Application.Interfaces;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Models;

namespace Promotion.Coupon.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonApplication _personApplication;
        private readonly INewsSendingApplication _newsSendingApplication;
        private readonly IVoucherApplication _voucherApplication;
        private readonly IConfigPromotionApplication _configPromotionApplication;

        public HomeController()
        {
            _personApplication = new PersonApplication();
            _newsSendingApplication = new NewsSendingApplication();
            _voucherApplication = new VoucherApplication();
            _configPromotionApplication = new ConfigPromotionApplication();
        }
        public ActionResult Index()
        {
            ViewBag.NumbersWinners = _voucherApplication.NumberCoupons();
            ViewBag.EndPromotion = _configPromotionApplication.GetByType("intimus").FirstOrDefault();
            return View();
        }      
        public ActionResult InitFlow()
        {
            ViewBag.NumbersCode = _voucherApplication.NumberCoupons();
            ViewBag.EndPromotion = _configPromotionApplication.GetByType("intimus").FirstOrDefault();
            return View();
        }

        
    }
}