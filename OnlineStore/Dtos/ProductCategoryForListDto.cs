using OnlineStore.Models;
using System.Collections.Generic;

namespace OnlineStore.Dtos
{
    public class ProductCategoryForListDto
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public ICollection<ProductSubCtgForItemListDto> SubCategory { get; set; }
    }
}