using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ProductDetails
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int TotalPrice { get; set; }
        public int ProductPrice { get; set; }
        public int ProductOrderQuantity { get; set; }
        public string OrderCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? ArrivedDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}