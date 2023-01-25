using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Covers.Dto
{
    public class UpdateCoverDto
    {
        public Guid Guid { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
    }
}
