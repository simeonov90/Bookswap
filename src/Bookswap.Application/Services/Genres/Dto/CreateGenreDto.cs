using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Genres.Dto
{
    public class CreateGenreDto
    {
        [Required]
        [StringLength(35, ErrorMessage = $"The Length of {nameof(Name)} must be less or equals than 35 characters")]
        public string Name { get; set; }
    }
}
