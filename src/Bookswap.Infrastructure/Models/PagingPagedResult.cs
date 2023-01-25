namespace Bookswap.Infrastructure.Extensions.Models
{
    public class PagingPagedResult<TResult>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int RecordNumber { get; set; }
        public List<TResult> Items { get; set; }
    }
}
