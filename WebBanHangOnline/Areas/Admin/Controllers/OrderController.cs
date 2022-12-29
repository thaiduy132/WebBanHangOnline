using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using PagedList;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? status, string searchString, int? page)
        {
            ;
            IQueryable<Order> ordersQuery = db.Orders.OrderByDescending(x => x.CreatedDate);
            var statusQuery = from m in db.Orders orderby m.Status select m.Status;
            if (!string.IsNullOrEmpty(searchString))
            {
                ordersQuery = ordersQuery.Where(s => s.Code.Contains(searchString));
            }
            if (status == -1 || status ==  1 || status == 2 || status == 3 || status == 4 )
            {
                ordersQuery = ordersQuery.Where(x => x.Status == status);
            }

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(ordersQuery.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham (int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult UpdateTT(int id,int paystatus,int orderstatus)
        {
            var item = db.Orders.Find(id);
            if(item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = paystatus;
                item.Status = orderstatus;
                //Status :Hủy, Đặt thành công, Đã chuyển hàng, Đang vận chuyển, Đã nhận hàng , None 
                //         -1        1              2                3                4          0
                if(item.Status == 2)
                {
                    item.ShippedDate = DateTime.Now;
                }
                if (item.Status == 3)
                {
                    item.DeliverDate = DateTime.Now;
                }
                if (item.Status == 4)
                {
                    item.ArrivedDate = DateTime.Now;
                }
                if(item.Status == -1)
                {
                    item.CancledDate = DateTime.Now;
                }
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.Entry(item).Property(x => x.Status).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "success", success = true });

            }
            return Json(new { message = "fail", success = false });

        }
    }
}