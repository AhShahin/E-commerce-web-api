using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class OrderStatus : BaseEntity
    {
        public int Id { get; set; }
        public Order_Status Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
