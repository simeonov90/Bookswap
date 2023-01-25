using Bookswap.Domain.Extensions.Entities;
using Bookswap.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text:
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Books.Dtos
{
    public class BookDto : AuditedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public Guid CoverId { get; set; }
        public Cover Cover { get; set; }
    }
}
