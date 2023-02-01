namespace Bookswap.Application.Services.Books.Dtos
{
    public class CreateBookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int GenreId { get; set; }
    }
}
