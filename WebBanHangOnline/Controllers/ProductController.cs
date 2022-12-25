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
            var items = db.Products.ToList();
            return View(items);
        }
        public ActionResult GetProductsByCategory(int cateId)
        {
            var items = db.Products.Where(x => x.ProductCategoryId == cateId).ToList();
            return View(items);
        }

    }
}