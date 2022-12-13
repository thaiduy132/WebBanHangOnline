using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(int? page)
        {

            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(items.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
    }
}