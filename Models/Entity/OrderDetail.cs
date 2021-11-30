using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Entity
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int OrderItemID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string BuyingType { get; set; }
        public string Comments { get; set; }

        public Order Order { get; set; }
        public OrderItem OrderItem { get; set; }

        private List<OrderItem> orderItems = new List<OrderItem>();
        public List<OrderItem> OrderItems 
        {
            get
            {
                return orderItems;
            }
            set
            {
                orderItems = value;
            }
        }
    }
}
