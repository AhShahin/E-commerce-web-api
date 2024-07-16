namespace OnlineStore.Dtos
{
    public class PopularProductsForListDto
    {
        public int ProductId { get; set; }
        public int ProductOptionId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

    }
}
