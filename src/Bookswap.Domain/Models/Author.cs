using System.ComponentModel.DataAnnotations;

namespace Bookswap.Domain.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
    }
}