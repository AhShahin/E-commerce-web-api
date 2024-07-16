using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public int Viewed { get; set; }
        public ProductCategory Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ProductSubCategory SubCategory { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        public Material Material { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required]
        public int StyleId { get; set; }
        public Style Style { get; set; }
        public ICollection<ProductOptions> ProductOptions { get; set; }
    }
}
