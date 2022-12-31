using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Authors.Dto
{
    public class CreateAuthorDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Author name can`t be null or whitespace!")]
        public string FullName { get; set; }
    }
}
