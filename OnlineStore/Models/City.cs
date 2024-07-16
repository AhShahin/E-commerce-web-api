using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class City : BaseEntity
    {
        public int Id { get; set; }
        public Cities Name { get; set; }
        public ICollection<Address> Adresses { get; set; }
    }
}
