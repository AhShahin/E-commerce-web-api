using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers
{
    public interface IQueryObject
    {
        int PageNumber { get; set; }
        byte PageSize { get; set; }
        string? OrderBy { get; set; }
        string? SearchTerm { get; set; }
        bool IsSortAscending { get; set; }
    }
}
