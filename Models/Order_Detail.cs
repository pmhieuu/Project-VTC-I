using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Order_Detail
    {
        public int ID_Order { get; set; }
        public int ID_Product { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}