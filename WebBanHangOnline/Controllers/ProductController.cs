using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            var items = db.Products.Where(x=>x.IsActive == true).ToList();
            return View(items);
        }
        public ActionResult Detail(int id)
        {
            var item = db.Products.Find(id);
            return View(item);
        }

    }
}