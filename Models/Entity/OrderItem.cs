using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Entity
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public bool Status { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
