using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data.Repos
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<Product>> GetProducts(Gender gender , ProductParams itemParams)
        {
            var items = Context.Products
                            .Include(i => i.Brand)
                            .Include(i => i.Material)
                            .Include(i => i.Style)
                            .Include(i => i.Category)
                            .Include(i => i.SubCategory)
                            .Include(i => i.ProductOptions)
                                .ThenInclude(po => po.Images)
                            .Include(i => i.ProductOptions)
                                .ThenInclude(po => po.Color)
                            .Include(i => i.ProductOptions)
                                .ThenInclude(po => po.ProductOptions_Sizes)
                                    .ThenInclude(po => po.Size)
                            .AsNoTracking()
                            .AsSplitQuery()
                            .AsQueryable();

            items = items.Where(i => i.Category.Name == gender);
            

            if (!string.IsNullOrEmpty(itemParams.SearchTerm))
            {
                items = items.Where(i => i.Description.Contains(itemParams.SearchTerm));
            }
             
            if (itemParams.PriceFrom.HasValue)
            {
                items = items.Where(i => i.Price >= itemParams.PriceFrom);
            }

            if (itemParams.PriceTo.HasValue)
            {
                items = items.Where(i => i.Price <= itemParams.PriceTo);
            }

            if (itemParams.CategoryName?.Length > 0)
            {
                var p = PredicateBuilder.False<Product>();
                foreach (var ctg in itemParams.CategoryName)
                {
                    p = p.Or(i => i.SubCategory.Name == ctg);
                }
                items = items.Where(p);
            }

            if (itemParams.BrandName?.Length > 0)
            {
                var p = PredicateBuilder.False<Product>();

                foreach (var brand in itemParams.BrandName)
                {
                    p = p.Or(i => i.Brand.Name == brand);
                }
                items = items.Where(p);
            }
            if (!string.IsNullOrEmpty(itemParams.MaterialName.ToString()))
            {
                items = items.Where(i => i.Material.Name == itemParams.MaterialName);
            }
            if (!string.IsNullOrEmpty(itemParams.StyleName))
            {
                items = items.Where(i => i.Style.Name.ToString() == itemParams.StyleName);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Product, object>>>()
            {
                ["Name"] = i => i.Description,
                ["Viewed"] = i => i.Viewed,
                ["CategoryName"] = i => i.SubCategory.Name,
            };
            items = items.ApplyOrdering(itemParams, columnsMap);

            return await PagedList<Product>.CreateAsync(items, itemParams.PageNumber, itemParams.PageSize);
        }

        public async Task<ProductForListDto?> GetProduct(int id)
        {
            
            return await Context.Products.Where(p => p.Id == id)
            .AsNoTracking()
            .ProjectTo<ProductForListDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> AddProductOptionStockBySize(IEnumerable<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                var cartItem = await Context.ProductOptions_Sizes
                                   .Where(ps => ps.ProductOptionsId == item.SelectedOptionId
                                        && ps.SizeId == item.SizeId).FirstOrDefaultAsync();
                item.Stock = cartItem!.Quantity;
            }

            return cartItems;
        }
        public async Task<IEnumerable<object>> GetPopularProducts(Gender gender)
        {
             var qu =await (from p in Context.Products
                          join pc in Context.ProductSubCategories
                              on p.SubCategoryId equals pc.Id
                          join pcs in Context.ProductCategories.Where(x => x.Name == gender)
                              on p.CategoryId equals pcs.Id
                          join po in Context.ProductOptions
                              on p.Id equals po.ProductId
                          join op in Context.OrderProducts
                              on po.Id equals op.ProductOptionsId
                          group new { p, po, op } by new
                          {
                              p.Name,
                              p.Id,
                              p.Price,
                              po.ProductId,
                              op.ProductOptionsId,
                              CategoryName = pcs.Name,
                              SubCategoryName = pc.Name
                          } into grp
                          orderby grp.Sum(g => g.op.Quantity) descending
                          select new 
                          {
                              ProductId = grp.Key.Id,
                              ProductOptionId = grp.Key.ProductOptionsId,
                              CategoryName = grp.Key.CategoryName.ToString(),
                              SubCategoryName = grp.Key.SubCategoryName.ToString(),
                              ProductName = grp.Key.Name,
                              grp.Key.Price,
                              Quantity = grp.Sum(p => p.op.Quantity)
                          }).Take(3).ToListAsync();
            return qu;
        }

        public async Task<IEnumerable<string>?> GetProductTitles(string searchTerm)
        {
            return await Context.Products.
                         Where(p => p.Name.Contains(searchTerm))
                         .Select(p => p.Name)
                         .ToListAsync();
        }

        public async Task<PagedList<object>> GetProductsWithLowQty(ProductParams itemParams)
        {
            var items = from i in Context.Products
                        where i.ProductOptions.Any(p => p.ProductOptions_Sizes.Select(p => p.Quantity).Sum() < 3)
                        select new
                        {
                            Id = i.Name,
                            po = from po in i.ProductOptions
                                 where po.ProductOptions_Sizes.Select(p => p.Quantity).Sum() < 3 
                                 group po.ProductOptions_Sizes by new { po.Id, po.SKU, po.IsMain } into grp
                                 select new
                                 {
                                     idm = grp.Key.Id,
                                     sku = grp.Key.SKU,
                                     isMain = grp.Key.IsMain,
                                     quantity = grp.SelectMany(p => p.Select(p => p.Quantity)).Sum()
                                 }
                        };
            return await PagedList<object>.CreateAsync(items, itemParams.PageNumber, itemParams.PageSize);
        }

        public async Task<IEnumerable<object>?> ProductsSoldLessThanThreeTimes()
        {

            var res = Context.Products.
                Include(p => p.ProductOptions).
                Where(p => p.ProductOptions.Any(c => c.OrderProducts.Any())).
                Select(p => new
                {
                    pId = p.Id,
                    pName = p.Name,
                    ProductOptions = p.ProductOptions.Where(p => p.OrderProducts.Sum(p => p.Quantity) < 3)
                                             .Select(p => new { p, q = p.OrderProducts.Sum(p => p.Quantity) })
                }).ToListAsync();

            return await res;
        }

        public async Task<IEnumerable<object>?> ProductsSoldInFirstQuarter()
        {

            //var query = await Context.Orders.GroupBy(o => o.UserId).OrderByDescending(o => o.Count()).Select(g => g.Key).FirstAsync().;
            var res = from p in Context.Products
                      join po in Context.ProductOptions
                      on p.Id equals po.ProductId
                      join op in Context.OrderProducts on po.Id equals op.ProductOptionsId
                      where
                      po.OrderProducts.Any() &&
                      op.CreatedOn >= new DateTime(2024, 01, 01) &&
                      op.CreatedOn <= new DateTime(2024, 03, 30)
                      select new { prodName = p.Name, op };


            return res;
        }

        public async Task<IEnumerable<object>?> ProductsWithTotalSoldProdOpt()
        {

            var res = Context.Products.
                Where(p => p.ProductOptions.Any(c => c.OrderProducts.Any())).
                Select(p => new {
                    pId = p.Id,
                    pName = p.Name,
                    po = p.ProductOptions.
                Select(po => new { po, quantity = po.OrderProducts.Sum(op => op.Quantity) })
                }).ToListAsync();


            return await res;
        }
    }
}
