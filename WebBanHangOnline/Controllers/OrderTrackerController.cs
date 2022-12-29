using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

            // Lấy chi tiết hàng của Order
            var productItems = from p in db.Products
                               join od in db.OrderDetails on p.Id equals od.ProductId
                               join o in db.Orders on od.ProductId equals id
                               select new
                               {
                                   p.Id,
                                   p.Quantity
                               };

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
                // Cập nhật lại số lượng sản phẩm của đơn hàng 
                //foreach(var item in orderDetails)
                //{
                    
                //}
                db.SaveChanges();
                code = new { success = true };
                return Json(code);
            }
            return Json(code);
        }


        
    }
}