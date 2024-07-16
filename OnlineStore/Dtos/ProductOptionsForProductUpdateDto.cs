namespace OnlineStore.Dtos
{
    public class ProductOptionsForProductUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
    }
}
