using OnlineStore.Helpers.QueryParams;
using OnlineStore.Helpers;
using OnlineStore.Models;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Data.Repos
{
    public class BrandsRepository : Repository<Brand>, IBrandRepository
    {
        public BrandsRepository(DataContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await Context.Brands.ToListAsync();    
            
        }
    
    
    }
}
