using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using Chart = ChartJSCore.Models.Chart;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class StasticController : Controller
    {
        // GET: Admin/Stastic

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //var items = from od in db.OrderDetails
            //            join o in db.Orders on od.OrderId equals o.Id
            //            join p in db.Products on od.ProductId equals p.Id
            //            group p by od.ProductId into g
            //            select new BestSeller
            //            {
            //                ProductID = g.Key,
            //                TotalSold = g.Sum(x => x.Quantity),
            //                ProductName = g.FirstOrDefault().Title,
            //                Image = g.FirstOrDefault().Image

            //            };
            //items.ToList();
            //return View(items);
            return View();
        }
        
        //public ActionResult Test()
        //{
        //    var items = from od in db.OrderDetails
        //                join o in db.Orders on od.OrderId equals o.Id
        //                join p in db.Products on od.ProductId equals p.Id
        //                group p by od.ProductId into g
        //                select new 
        //                {
        //                    ProductID = g.Key,
        //                    ProductName = g.FirstOrDefault().Title,
        //                    Image = g.FirstOrDefault().Image

        //                };
        //    items.ToList();

        //    List<object> list = new List<Object>();
        //    list.Add("test");
        //    list.Add("test");

        //    var result = new BestSeller();
        //    result.tbl1 = (List<AllowAnonymousAttribute>)items;
        //    result.tbl2 = (List<object>)list;
        //    return View(result);
        //}
    }
}