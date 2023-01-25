using Bookswap.Application.Services.Covers;
using Microsoft.AspNetCore.Mvc;
using Bookswap.Application.Services.Covers.Dto;
using Bookswap.Application.Extensions.ExceptionMessages;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Authors;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverController : ControllerBase
    {
        private readonly ICoverService coverService;
        private readonly ILogger<CoverController> logger;

        public CoverController(ICoverService coverService, ILogger<CoverController> logger)
        {
            this.coverService = coverService;
            this.logger = logger;
        }

        // POST: api/Cover
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCoverDto createCoverDto)
        {

            try
            {
                var coverDto = await coverService.CreateAsync(createCoverDto);
                if (coverDto.UnSupportedFileType is not null)
                {
                    logger.LogWarning($"{coverDto.UnSupportedFileType}, {nameof(Create)}");

                    return BadRequest(coverDto.UnSupportedFileType);
                }

                return Ok(coverDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"{LogErrorExcepitonMessage.SomethingWentWrong(nameof(Create), ex.Message)}");
                return Problem(CommonExceptionMessage.SomethingWentWrongContactSupport(nameof(Create)), statusCode: 500);
            }
        }

        // GET: api/Cover
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoverDto>>> GetCovers()
        {
            return Ok(await coverService.GetAllAsync());
        }
    }
}
