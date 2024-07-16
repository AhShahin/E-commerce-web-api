using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Helpers.QueryParams
{
    public class AddressParams : IQueryObject
    {
        private const byte MaxPageSize = 50;
        private byte pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public byte PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string? OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public bool IsSortAscending { get; set; }

        public string? StreetAddress { get; set; }
        public string? Postcode { get; set; }
        public Cities? City { get; set; }
        public Countries? Country { get; set; }
        public bool IsShippingAddress { get; set; }
        public bool IsBillingAddress { get; set; }
    }
}
