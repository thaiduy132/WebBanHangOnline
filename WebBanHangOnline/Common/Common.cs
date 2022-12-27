using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Common
{
    public class Common
    {
        public string formatVND(int money)
        {
           
            return money.ToString("#,##0");
        }
    }
}