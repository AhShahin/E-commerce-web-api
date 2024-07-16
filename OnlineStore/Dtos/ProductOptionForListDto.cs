using OnlineStore.Models;
using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class ProductOptionForListDto
    {
        public int Id { get; set; }
        //public int Quantity { get; set; }
        public string Color { get; set; }
        public ICollection<Sizes> sizes { get; set; }
        public ICollection<Image> Images { get; set; }
        public ProductsListForOrderDTO Product { get; set; }
    }
}
