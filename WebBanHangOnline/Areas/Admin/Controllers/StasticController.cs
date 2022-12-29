using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class StasticController : Controller
    {
        // GET: Admin/Stastic

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var productItems = from p in db.Products
                               join od in db.OrderDetails on p.Id equals od.ProductId
                               join o in db.Orders on od.OrderId equals o.Id
                               where o.Status == -1
                               select new 
                               {
                                   p.Id,
                                   od.OrderId,
                                   od.Quantity
                               };

            ViewBag.Products = productItems;

            return View();
        }
    }
}