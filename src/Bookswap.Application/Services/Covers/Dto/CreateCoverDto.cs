using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Covers.Dto
{
    public class CreateCoverDto
    {
        [Required]
        public IFormFile FormFile { get; set; }
        public int BookId { get; set; }
    }
}
