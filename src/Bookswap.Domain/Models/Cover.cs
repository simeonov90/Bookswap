using System.ComponentModel.DataAnnotations;

namespace Bookswap.Domain.Models
{
    public class Cover
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Bytes { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        //public int BookId { get; set; }
        //[ForeignKey(nameof(BookId))]
        //public Book Book { get; set; }
    }
}
