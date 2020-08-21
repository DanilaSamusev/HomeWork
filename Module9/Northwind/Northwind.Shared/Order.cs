using System;

namespace Northwind.Shared
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public OrderStatus Status { get; set; }
    }
}
