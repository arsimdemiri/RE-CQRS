using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            int page,
            int pageSize)
            where TModel : class
        {

            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = pageSize;

            var totalItemsCountTask = query.CountAsync();

            var startRow = (page - 1) * pageSize;
            paged.Items = await query
                       .Skip(startRow)
                       .Take(pageSize)
                       .ToListAsync();

            paged.TotalItems = await totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)pageSize);

            return paged;
        }
    }
}
