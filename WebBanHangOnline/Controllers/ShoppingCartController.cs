using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.Items);
            }
            return View();
        }
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.Items);
            }
            return View();
        }
        public ActionResult Partial_Checkout()
        {
            return PartialView();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult Partial_Items_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { success = false, code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart != null)

                {
                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,

                    }));
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    order.Email = req.Email;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    //Send Email 
                    var strSanPham = "";
                    var thanhtien = 0;
                    var TongTien = 0;
                    foreach(var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>"+sp.ProductName +"</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + sp.TotalPrice+ "</td>";

                        strSanPham += "</tr>";
                        thanhtien += sp.Price * sp.Quantity;
                    }
                    TongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/mail/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));

                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", thanhtien.ToString());
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", TongTien.ToString());
                    contentCustomer = contentCustomer.Replace("{{TenKH}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{SoDienThoai}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);

                    contentCustomer = contentCustomer.Replace("{{Email}}", req.Email);

                    WebBanHangOnline.Models.Common.SendEmail.sendEmail("AgletShop", "Đơn hàng#" + order.Code, contentCustomer.ToString(), req.Email);

                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json(code);
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart!= null)
            {
                return Json(new {  count = cart.Items.Count },JsonRequestBehavior.AllowGet);

            }
            return Json(new { count = 0 },JsonRequestBehavior.AllowGet);    
        }
        [HttpPost]
        public async Task<ActionResult> AddToCart(int id, int quantity)
        {
            var code = new { success = false, msg = "Số lượng trong kho không đủ !!", code = -1 ,count = 0};
            var db = new ApplicationDbContext();
            Product product = await db.Products.FindAsync(id);
            var productQuantity = product.Quantity;
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id); 
            if(checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem()
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Quantity = quantity

                };
                // TH1 : Sản phẩm chưa có trong giỏ hàng 
                // Nếu số lượng add > số lượng trong kho => false 
                // Nếu số lượng trong cart hiện tại > số lượng trong kho => false
                if(quantity > productQuantity)
                {
                    return Json(code);
                }
                // Nếu sản p
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (int)checkProduct.PriceSale;
                }
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);

                //Số lượng của sản phẩm trong cart hiện tại 

                var productQuantityCart = cart.Items.FirstOrDefault(x => x.ProductId == id).Quantity;

                //Nếu số lượng sp add thêm vào cart <= sl kho nhưng số lượng trong cart > số lượng kho => false
                //Update lại số lượng trong cart về số lượng cũ
                if (quantity <= productQuantity && productQuantityCart > productQuantity)
                {
                    cart.Decrease(item, quantity);
                    return Json(code);
                }

                    Session["Cart"] = cart;
                code = new { success = true, msg = "Thêm sản phẩm thành công", code = 1 ,count = cart.Items.Count};

            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Update(int id,int quantity)
        {
            var db = new ApplicationDbContext();
            Product product =  db.Products.Find(id);
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            // Sản phẩm được update
            var UpdatedItem = cart.Items.SingleOrDefault(x => x.ProductId == id);
            // Số lượng sản phẩm trong kho hiện tại
            var productQuantity = product.Quantity;
            // Số lượng sản phẩm trong cart hiện tại (SL cũ)
            var productQuantityCart = cart.Items.FirstOrDefault(x => x.ProductId == id).Quantity;
            if (cart != null)
            {
                //Cập nhật lại số lượng
                cart.UpdateQuantity(id, quantity);
                // Số lượng mới 
                var UpdatedQuantityCart = cart.Items.FirstOrDefault(x => x.ProductId == id).Quantity;
                //Số lượng sản phẩm đã được thêm vào  = SL mới - SL cũ 
                var AddedQuantity = UpdatedQuantityCart - productQuantityCart;

                // Nếu số lượng trong kho bé hơn số lượng sp trong cart
                if (productQuantity < UpdatedQuantityCart)
                {
                    // Set số lượng sản phẩm trong cart về SL cũ 
                    cart.Decrease(UpdatedItem, AddedQuantity);
                    return Json(new { success = false });
                }
                // Nếu số lượng trong cart > số lượng trong kho 
                // Set lại số lượng cũ 

                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
        public ActionResult Delete(int id)
        {
            var code = new { success = false, msg = "", code = -1, count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if(checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { success = true, msg = "", code = -1, count = cart.Items.Count };

                }
            }
            return Json(code);
        }
    }
}