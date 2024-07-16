using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class Season : BaseEntity
    {
        public int Id { get; set; }
        public Seasons Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
