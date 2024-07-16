using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers.QueryParams
{
    public class OrderParams : IQueryObject
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

        public string? PaymentMethod { get; set; }
        public string? Type { get; set; }
        public string? Owner { get; set; }
        public string? Number { get; set; }
        public string? OrdersStatus { get; set; }
        public string? ShippingMethod { get; set; }
        public string? Comments { get; set; }
    }
}
