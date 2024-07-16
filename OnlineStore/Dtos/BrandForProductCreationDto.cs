using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class BrandForProductCreationDto
    {
        public int Id { get; set; }
        public Brands Name { get; set; }
    }
}
