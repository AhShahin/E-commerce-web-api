using OnlineStore.Models;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class ProductOptionsForItemsList
    {
        public int Id { get; set; }
        public ColorForListDto Color { get; set; }
        public IEnumerable<SizeForListDto> sizes { get; set; }
        public ICollection<ImagesForListDto> Images { get; set; }
        public string SKU { get; set; }
    }
}