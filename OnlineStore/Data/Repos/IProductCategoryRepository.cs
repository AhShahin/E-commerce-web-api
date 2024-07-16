using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        public Task<IEnumerable<ProductCategory>> getCategories();
    }
}
