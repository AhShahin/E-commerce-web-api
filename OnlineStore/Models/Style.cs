using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class Style : BaseEntity
    {
        public int Id { get; set; }
        public Styles Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
