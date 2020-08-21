using System;
using System.Collections.Generic;

namespace Northwind.Shared
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public OrderStatus Status { get; set; }

        public List<Product> products;
    }
}
