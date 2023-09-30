using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PharmacyInventory_Shared.RequestParameter.Common
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new()
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int) Math.Ceiling( count / (double)pageSize)

            };
            AddRange(items);
        }
        public static async Task<PagedList<T>>GetPagination(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)              
                .ToListAsync();
            return  new   PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
 