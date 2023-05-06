using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {   
        public int ID_Order { get; set; }
        public int ID_Customer { get; set; }
        public int ID_Staff { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Phone { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        
    }
}