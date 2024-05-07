namespace webapi.Paging
{
    public abstract class BasePaging<T> where T : class
    {
        public BasePaging()
        {
        }

        public BasePaging(int totalItems, IEnumerable<T> items)
        {
            TotalItems = totalItems;
            Items = items;
        }

        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }

    public class PagedResultDto<T> : BasePaging<T> where T : class
    {
        public PagedResultDto() : base()
        {
        }

        public PagedResultDto(int totalItems, IEnumerable<T> items) : base(totalItems, items)
        {
        }
    }
}
