using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class ShippingMethod : BaseEntity
    {
        public int Id { get; set; }
        public ShippingMethods Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
