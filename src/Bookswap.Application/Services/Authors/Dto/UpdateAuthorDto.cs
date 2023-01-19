using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Authors.Dto
{
    public class UpdateAuthorDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string FullName { get; set; }
    }
}
