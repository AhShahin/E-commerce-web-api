﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items); // add entity items to the end of the list
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> entity,
            int pageNumber, int pageSize)
        {
            var count = await entity.CountAsync();
            var items = await entity.Skip((pageNumber) * pageSize).Take(pageSize).ToListAsync();
            // return the list with the other extended properties
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
