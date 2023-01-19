namespace Bookswap.Infrastructure.Extensions.Models
{
    public class PagingQueryParameters
    {
        private int pageSize = 10;
        public int StartIndex { get; set; }
        public int PageNumber { get; set; }
        public int PageSize 
        {
            get 
            {
                return pageSize;
            }
            set
            {
               this.pageSize = value;
            }
        }
    }
}
