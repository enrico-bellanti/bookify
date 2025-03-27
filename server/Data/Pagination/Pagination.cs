namespace Bookify.Data.Pagination
{
    public enum SortDirection
    {
        ASC,
        DESC
    }
    public class Sort
    {
        public string Column { get; }
        public SortDirection Direction { get; }

        private Sort(string column, SortDirection direction)
        {
            Column = column;
            Direction = direction;
        }

        public static Sort By(SortDirection direction, string column)
        {
            return new Sort(column, direction);
        }
    }
    public class PageRequest
    {
        public int Page { get; }
        public int Size { get; }
        public Sort Sort { get; }

        private PageRequest(int page, int size, Sort sort)
        {
            Page = page < 0 ? 0 : page;
            Size = size < 1 ? 25 : size;
            Sort = sort;
        }

        public static PageRequest Of(int page, int size, Sort sort)
        {
            return new PageRequest(page, size, sort);
        }

        public static PageRequest Of(int page, int size)
        {
            return new PageRequest(page, size, Sort.By(SortDirection.ASC, "id"));
        }
    }

    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPrevious => PageNumber > 0;
        public bool HasNext => PageNumber + 1 < TotalPages;
    }
}
