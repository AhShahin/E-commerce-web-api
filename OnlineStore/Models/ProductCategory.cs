using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class ProductCategory : BaseEntity
    {
        // Men and Women
        public int Id { get; set; }
        [Required]
        public Gender Name { get; set; }
        public ICollection<ProductSubCategory> SubCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
