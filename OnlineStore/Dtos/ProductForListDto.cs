using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class ProductForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Material { get; set; }
        public string Style { get; set; }
        public IEnumerable<ColorForListDto> Colors { get; set; } 
        public ProductOptionsForItemsList SelectedProductOption { get; set; }
        public ICollection<ProductOptionsForItemsList> ProductOptions { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

    }
}
