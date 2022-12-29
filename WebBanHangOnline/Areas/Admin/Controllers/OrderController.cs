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
        public ActionResult UpdateTT(int id)
        {
            var currentStatus = db.Orders.SingleOrDefault(x => x.Id == id).Status;
            var item = db.Orders.Find(id);
            if(item != null)
            {
                //
                db.Orders.Attach(item);
                //Status :Hủy, Đặt thành công, Đã chuyển hàng, Đang vận chuyển, Đã nhận hàng , None 
                //         -1        1              2                3                4          0
                if(currentStatus == -1)
                {
                    return Json(new { success = false });
                }
                currentStatus++; // example : nếu status == 1 => currentStatus == 2  

                if (currentStatus == 1)
                {
                    item.CreatedDate = DateTime.Now;
                }
                if (currentStatus == 2)
                {
                    item.ShippedDate = DateTime.Now;
                }
                if (currentStatus == 3)
                {
                    item.DeliverDate = DateTime.Now;
                }
                if (currentStatus == 4)
                {
                    item.TypePayment = 2;
                    item.ArrivedDate = DateTime.Now;
                }
                if(currentStatus == -1)
                {
                    item.CancledDate = DateTime.Now;
                }
                
                if (currentStatus > 4)
                {
                    currentStatus--;
                }
                item.Status = currentStatus;


                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.Entry(item).Property(x => x.Status).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "success", success = true });

            }
            return Json(new { message = "fail", success = false });

        }
        public ActionResult Cancel(int id)
        {
            var code = new { success = false };
            Order order = db.Orders.SingleOrDefault(x => x.Id == id);
            var currentStatus = db.Orders.Find(id).Status;
            // Nếu sản phẩm đã bàn giao cho bên vận chuyển
            // thì không cho phép hủy đơn hàng
            if (currentStatus > 1 && currentStatus <= 4) 
            {
                return Json(code);
            }
            // Nếu sản phẩm chưa bàn giao thì cho phép hủy
            else if(currentStatus >= 0 && currentStatus <= 1)
            {
                // Cập nhật lại trạng thái đơn hàng
                order.Status = -1;
                db.SaveChanges();
                code = new { success = true }; 

                return Json(code);
            }
            return Json(code);
        }
    }
}