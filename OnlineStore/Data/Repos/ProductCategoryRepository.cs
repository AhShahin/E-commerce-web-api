using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DataContext context) : base(context)
        { }

        public async Task<IEnumerable<ProductCategory>> getCategories()
        {
            //await Context.ProductCategories.FromSqlRaw($"Execute spCategoriesTopBottom").ToListAsync();

            return await Context.ProductCategories
                .Include(pc => pc.SubCategory)
                .ToListAsync();
        }
    }
}
