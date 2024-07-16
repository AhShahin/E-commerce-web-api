using OnlineStore.Dtos;
using System;
using System.Linq;

namespace OnlineStore.Models
{
    public class CartItem : BaseEntity
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public ImagesForListDto Image { get; set; }
        public string Size { get; set; }
        public int SizeId { get; set; }
        public int Stock { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string ProductSubCtg { get; set; }
        public string Sku { get; set; }
        public int SelectedOptionId { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = Price * Quantity;

                return totalprice;
            }
        }

        public int[] MaxQuantityAllowed
        {
            get
            {
                int maxQuantity = Stock < 10 ? Stock: 10;

                return Enumerable.Range(1, maxQuantity).ToArray();
            }
        }
    }
}
