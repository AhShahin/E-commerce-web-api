using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class Material : BaseEntity
    {
        public int Id { get; set; }
        public Materials Name { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
