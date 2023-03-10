using Bookswap.Application.Services.Genres;
using Microsoft.AspNetCore.Mvc;
using Bookswap.Application.Services.Genres.Dto;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Application.Extensions.ExceptionMessages;
using Microsoft.AspNetCore.Authorization;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Authors;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;
        private readonly ILogger<GenreController> logger;

        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            this.genreService = genreService;
            this.logger = logger;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            return Ok(await genreService.GetAllAsync());    
        }

        // GET: api/Genre/?StartIndex=0&PageSize=10&PageNumber=1
        [HttpGet($"{nameof(GetPagedGenres)}")]
        public async Task<ActionResult<PagingPagedResult<GenreDto>>> GetPagedGenres([FromQuery] PagingQueryParameters queryParameters)
        {
            return Ok(await genreService.GetAllAsync(queryParameters));
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var entity = await genreService.GetById(id);
            if (entity is null)
            {
                logger.LogWarning(LogWarningExceptionMessage.EntityRecordDoesNotExists(nameof(GetGenreById), id));
                return NotFound();
            }

            return Ok(entity);
        }

        // POST: api/Genre
        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create([FromBody] CreateGenreDto createGenreDto)
        {
            try
            {
                return Ok(await genreService.CreateAsync(createGenreDto));
            }
            catch (Exception ex)
            {
                logger.LogError(LogErrorExcepitonMessage.SomethingWentWrong(nameof(Create), ex.Message));
                return Problem(CommonExceptionMessage.SomethingWentWrongContactSupport(nameof(Create)), statusCode: 500);
            }
            
        }

        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGenreDto updateGenreDto)
        {
            if (id != updateGenreDto.Id)
            {
                logger.LogWarning(LogWarningExceptionMessage.UpdateParametersAreNotSame(nameof(Update), id, updateGenreDto.Id));
                return BadRequest();
            }

            try
            {
                await genreService.UpdateAsync(updateGenreDto);
            }
            catch (Exception ex)
            {
                logger.LogError(LogErrorExcepitonMessage.SomethingWentWrong(nameof(Update), ex.Message));
            }

            return NoContent();
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isExists = await genreService.Exists(id);
                if (isExists is false)
                {
                    logger.LogWarning(LogWarningExceptionMessage.EntityRecordDoesNotExists(nameof(Delete), id));
                    return NotFound();
                }

                await genreService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(LogErrorExcepitonMessage.SomethingWentWrong(nameof(Delete), ex.Message));
                return Problem(CommonExceptionMessage.SomethingWentWrongContactSupport(nameof(Delete)), statusCode: 500);
            }
        }
    }
}
