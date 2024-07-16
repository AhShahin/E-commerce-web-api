using static OnlineStore.Helpers.Enums;
using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Size : BaseEntity
    {
        public int Id { get; set; }
        public Sizes Name { get; set; }
        public ICollection<ProductOptions_Sizes> ProductOptions_Sizes { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
