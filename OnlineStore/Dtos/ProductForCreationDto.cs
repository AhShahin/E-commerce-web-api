using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class ProductForCreationDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int MaterialId { get; set; }
        public int BrandId { get; set; }
        public int StyleId { get; set; }
        public int SeasonId { get; set; }
        public ICollection<ProductOptionsForProductCreationDto> ProductOptions { get; set; }
    }
}
