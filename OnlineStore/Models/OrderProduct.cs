using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class OrderProduct : BaseEntity
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public int SizeId { get; set; }
        public Size? Size { get; set; }  
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductOptionsId { get; set; }
        public ProductOptions? ProductOption { get; set; }
    }
}
