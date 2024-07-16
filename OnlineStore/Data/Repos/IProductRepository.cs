using Microsoft.AspNetCore.Mvc;
using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data.Repos
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<object>> GetProductsWithLowQty(ProductParams itemParams);
        Task<PagedList<Product>> GetProducts(Gender gender, ProductParams itemParams);
        Task<ProductForListDto?> GetProduct(int id);
        Task<IEnumerable<string>?> GetProductTitles(string searchTerm);
        Task<IEnumerable<object>> GetPopularProducts(Gender gender);
        Task<IEnumerable<CartItem>> AddProductOptionStockBySize(IEnumerable<CartItem>? cartItems);
        Task<IEnumerable<object>?> ProductsSoldInFirstQuarter();
        Task<IEnumerable<object>?> ProductsSoldLessThanThreeTimes();
        Task<IEnumerable<object>?> ProductsWithTotalSoldProdOpt();
    }
}
