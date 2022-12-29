using Microsoft.Owin.BuilderProperties;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models
{
    public class ProductOrderODetail
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public int OrderId { get; set; }

        public int Status { get;set; }
        public string CustomerName  { get; set; }  
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public DateTime? ArrivedDate { get; set; }

    }
}
