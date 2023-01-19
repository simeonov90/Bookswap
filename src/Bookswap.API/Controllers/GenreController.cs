using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Authors;
using Bookswap.Application.Services.Genres;
using Microsoft.AspNetCore.Mvc;
using Bookswap.Application.Services.Genres.Dto;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // POST: api/Genre
        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create([FromBody] CreateGenreDto createGenreDto)
        {
            return Ok(await genreService.CreateAsync(createGenreDto));
        }
    }
}
