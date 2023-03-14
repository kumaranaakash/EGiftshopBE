﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGiftshopBE.Models
{
    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
       
        public string UnitPrice { get; set; }
        public string Discount { get; set; }
        public string Quantity { get; set; }
      
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }
    }
}
