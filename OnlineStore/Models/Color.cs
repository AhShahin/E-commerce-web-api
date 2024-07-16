using static OnlineStore.Helpers.Enums;
using System.Collections.Generic;
using System;

namespace OnlineStore.Models
{
    public class Color : BaseEntity
    {
        public int Id { get; set; }
        public Colors Name { get; set; }
        public string Value { get; set; }
        public ICollection<ProductOptions> ProductOptions { get; set; }
    }
}
