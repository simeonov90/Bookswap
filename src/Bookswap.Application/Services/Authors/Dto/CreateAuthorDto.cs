using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Authors.Dto
{
    public class CreateAuthorDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Author name can`t be null or whitespace!")]
        [StringLength(35, ErrorMessage = $"The Length of {nameof(FullName)} must be less or equals than 35 characters")]
        public string FullName { get; set; }
    }
}
