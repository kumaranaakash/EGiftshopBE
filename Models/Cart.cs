using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGiftshopBE.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }

    }
}
