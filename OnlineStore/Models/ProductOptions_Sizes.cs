using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class ProductOptions_Sizes : BaseEntity
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public int Quantity { get; set; }
        public ProductOptions ProductOption { get; set; }
        public int ProductOptionsId { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
    }
}
