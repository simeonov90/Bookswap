using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Genres.Dto
{
    public class UpdateGenreDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
