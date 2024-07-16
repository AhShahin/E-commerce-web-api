using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace OnlineStore.Models
{
    public class ProductOptions : BaseEntity
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public bool IsMain { get; set; }
        public ICollection<ProductOptions_Sizes> ProductOptions_Sizes { get; set; }
        public Color Color { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<Image> Images { get; set; }

    }
}