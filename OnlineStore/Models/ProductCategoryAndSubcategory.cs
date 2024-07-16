using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class ProductCategoryAndSubcategory : BaseEntity
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductSubCategoryId { get; set; }
        public ProductSubCategory ProductSubCategory { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
