using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers.QueryParams
{
    public class UserParams : IQueryObject
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

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Type { get; set; }
        public string? Gender { get; set; }
        public int MinAge { get; set; } = 0;
        public int MaxAge { get; set; } = 0;
    }
}
