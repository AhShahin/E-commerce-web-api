using OnlineStore.Models;
using System.Collections.Generic;
using System;

namespace OnlineStore.Dtos
{
    public class ProductOptionsForProductCreationDto
    {
        public int Quantity { get; set; }
        public string SKU { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
    }
}
