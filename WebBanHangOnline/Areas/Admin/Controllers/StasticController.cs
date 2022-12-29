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
            List<Product> products = db.Products.ToList();
            List<OrderDetail> orderDetails = db.OrderDetails.ToList();
            List<Order> orders = db.Orders.ToList();


            return View();
        }
    }
}