using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Authors;
using Bookswap.Application.Services.Books;
using Bookswap.Application.Services.Books.Dtos;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookswap.Application.Extensions.ExceptionMessages;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly ILogger<BookController> logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            return Ok(await bookService.GetAllAsync());
        }

        // GET: api/Book/GetPagedAuthors/?StartIndex=0&PageSize=10&PageNumber=1
        [HttpGet($"{nameof(GetPagedAuthors)}")]
        public async Task<ActionResult<PagingPagedResult<BookDto>>> GetPagedAuthors([FromQuery] PagingQueryParameters queryParameters)
        {
            return Ok(await bookService.GetAllAsync(queryParameters));
        }

        // Get: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBookById(int id)
        {
            var entity = await bookService.GetById(id);
            if (entity is null)
            {
                logger.LogWarning(LogWarningExceptionMessage.EntityRecordDoesNotExists(nameof(GetBookById), id));
                return NotFound();
            }

            return Ok(entity);
        }

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto createBookDto)
        {
            return Ok(await bookService.CreateAsync(createBookDto));
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto updateBookDto)
        {
            if (id != updateBookDto.Id)
            {
                logger.LogWarning(LogWarningExceptionMessage.UpdateParametersAreNotSame(nameof(Update), id, updateBookDto.Id));
                return BadRequest();
            }

            try
            {
                await bookService.UpdateAsync(updateBookDto);
            }
            catch (Exception ex)
            {
                logger.LogError(LogErrorExcepitonMessage.SomethingWentWrong(nameof(Update), ex.Message));
            }

            return NoContent();
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isExists = await bookService.Exists(id);
                if (isExists is false)
                {
                    logger.LogWarning(LogWarningExceptionMessage.EntityRecordDoesNotExists(nameof(Delete), id));
                    return NotFound();
                }

                await bookService.DeleteAsync(id);
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
