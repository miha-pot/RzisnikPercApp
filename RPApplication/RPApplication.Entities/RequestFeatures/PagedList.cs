using Microsoft.EntityFrameworkCore;

namespace RPApplication.Entities.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items,
                         int count,
                         int pageNumber,
                         int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,
                                                           int pageNumber,
                                                           int pageSize)
        {
            var count = await source.CountAsync(); // First DB call: count records
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(); // Second DB call: get one page of data

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static PagedList<TDestination> ToPagedList<TSource, TDestination>(PagedList<TSource> source,
                                                                                 IEnumerable<TDestination> destinationItems)
        {
            return new PagedList<TDestination>(destinationItems.ToList(),
                                               source.MetaData.TotalCount,
                                               source.MetaData.CurrentPage,
                                               source.MetaData.PageSize);
        }
    }

    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
