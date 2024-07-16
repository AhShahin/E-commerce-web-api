using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class ProductSubCategory : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public Categories Name { get; set; }
        public ProductCategory Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
