using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();  
        }
        public void AddToCart(ShoppingCartItem item , int quantity)
        {
            var checkExist = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if(checkExist != null)
            {
                checkExist.Quantity += quantity;
                checkExist.TotalPrice = checkExist.Price * checkExist.Quantity;
            }
            else
            {
                Items.Add(item);
            }

        }
        public void Remove(int id)
        {
            var checkExist = Items.SingleOrDefault(x => x.ProductId == id);
            if(checkExist != null)
            {
                Items.Remove(checkExist);

            }
        }
        public void UpdateQuantity(int id , int quantity)
        {
            var checkExists = Items.SingleOrDefault(x => x.ProductId == id);
            if(checkExists != null)
            {
                checkExists.Quantity = quantity;
                checkExists.TotalPrice = checkExists.Quantity * checkExists.Price;

            }
        }
        public int GetTotalPrice()
        {
            return Items.Sum(x => x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();  
        }

    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }


    }
}