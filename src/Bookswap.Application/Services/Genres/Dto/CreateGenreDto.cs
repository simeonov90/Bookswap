using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Genres.Dto
{
    public class CreateGenreDto
    {
        [Required]
        public string Name { get; set; }
    }
}
