using OnlineStore.Helpers;
using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }
        public Brands Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
