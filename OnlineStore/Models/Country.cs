using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class Country : BaseEntity
    {
        public int Id { get; set; }
        public Countries Name { get; set; }
        public ICollection<Address> Adresses { get; set; }
    }
}
