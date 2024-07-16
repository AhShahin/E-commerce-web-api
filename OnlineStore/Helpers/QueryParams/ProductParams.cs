using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Helpers.QueryParams
{
    public class ProductParams : IQueryObject
    {
        private const byte MaxPageSize = 50;
        private byte pageSize = 10;
        public int PageNumber { get; set; } = 0;
        public byte PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string? OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public bool IsSortAscending { get; set; }

        public string? Name { get; set; }
        public Brands[]? BrandName { get; set; } = Array.Empty<Brands>();
        public Materials? MaterialName { get; set; }
        public string? StyleName { get; set; }
        public string? Color { get; set; }
        public Categories[]? CategoryName { get; set; } = Array.Empty<Categories>();
        public int? Viewed { get; set; } = 0;
        public bool Status { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
