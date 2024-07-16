using OnlineStore.Models;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class SizeForListDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int? ProdOptId { get; set; }
    }
}
