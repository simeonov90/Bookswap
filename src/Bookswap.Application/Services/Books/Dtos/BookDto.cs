using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Covers.Dto;
using Bookswap.Application.Services.Genres.Dto;
using Bookswap.Domain.Extensions.Entities;
using Bookswap.Domain.Models;

namespace Bookswap.Application.Services.Books.Dtos
{
    public class BookDto : AuditedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AuthorDto Author { get; set; }
        public GenreDto Genre { get; set; }
        public CoverDto Cover { get; set; }
    }
}
