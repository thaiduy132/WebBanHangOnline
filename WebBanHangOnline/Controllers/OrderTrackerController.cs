using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class OrderTrackerController : Controller
    {
        // GET: OrderTracker
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("GetOrder", "OrderTracker", new { searchString = searchString });
            }
            return View();
        }

        public ActionResult GetOrder(string searchString)
        {
            var order = db.Orders.SingleOrDefault(x=>x.Code == searchString);
            return View(order);
        }
        
    }
}