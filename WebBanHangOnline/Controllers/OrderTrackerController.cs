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
        
        public ActionResult CancelOrder(int id)
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
            else if (currentStatus >= 0 && currentStatus <= 1)
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