using OnlineStore.Helpers.QueryParams;
using OnlineStore.Helpers;
using OnlineStore.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineStore.Data.Repos
{
    public interface IBrandRepository: IRepository<Brand>
    {
        Task<IEnumerable<Brand>> GetBrands();
    }
}
