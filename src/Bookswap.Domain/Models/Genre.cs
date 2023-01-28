using System.ComponentModel.DataAnnotations;

namespace Bookswap.Domain.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; }
    }
}
