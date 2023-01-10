using Bookswap.Application.Services.Authors;
using Bookswap.Application.Services.Authors.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly ILogger<AuthorController> logger;

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            this.authorService = authorService;
            this.logger = logger;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            return Ok(await authorService.GetAllAsync());
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            var entity = await authorService.GetById(id);
            if (entity is null) return NotFound();
            
            return Ok(entity);
        }

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            return Ok(await authorService.CreateAsync(createAuthorDto));
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthorDto updateAuthorDto)
        {
            if (id != updateAuthorDto.Id) return BadRequest();

            try
            {
                await authorService.UpdateAsync(updateAuthorDto);
            }
            catch (Exception ex)
            {

                logger.LogError($"Something went wrong with Update: ${ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isExists = await authorService.Exists(id);
                if (isExists is false)
                {
                    logger.LogWarning($"Author with id={id} does not exists.");
                    return NotFound();
                }

                await authorService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong with Delete: ${ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Author/keyword/searchedkeyword
        [HttpGet("keyword/{keyword}")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsByKeyword(string keyword)
        {
            return Ok(await authorService.GetByKeyword(keyword));
        }
    }
}
