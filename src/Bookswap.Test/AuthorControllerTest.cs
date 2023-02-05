using Bookswap.API.Controllers;
using Bookswap.Application.Services.Authors;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.UOW;
using Bookswap.Infrastructure.UOW.IUOW;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;

namespace Bookswap.Test
{
    public class AuthorControllerTest
    {
        private readonly Mock<IAuthorService> authorServiceMock;

        public AuthorControllerTest()
        {
            authorServiceMock = new Mock<IAuthorService>();
        }

        [Fact]
        public async Task ShouldGetAuthorsList()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.GetAllAsync())
                .ReturnsAsync(authors);
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorsResult = await authorController.GetAuthors();

            // assert
            Assert.NotNull(authorsResult);
        }

        [Fact]
        public async Task ShouldGetAuthorById()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.GetById(1))
                .ReturnsAsync(authors[0]);
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorResult = await authorController.GetAuthorById(1);
            var viewResult = Assert.IsType<OkObjectResult>(authorResult.Result);

            // assert
            Assert.NotNull(authorResult);
            Assert.Equal(typeof(OkObjectResult), viewResult.GetType());
        }

        [Fact]
        public async Task ShouldGetAuthorByIdReturnNotFound()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.GetById(0));
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorResult = await authorController.GetAuthorById(0);
            var viewResult = Assert.IsType<NotFoundResult>(authorResult.Result);

            // assert
            Assert.Equal(typeof(NotFoundResult), viewResult.GetType());
        }

        [Fact]
        public async Task ShouldCreateAuthor()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.CreateAsync(new CreateAuthorDto() { FullName = authors[1].FullName }));
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorResult = await authorController.Create(new CreateAuthorDto() { FullName = authors[1].FullName });
            var viewResult = Assert.IsType<OkObjectResult>(authorResult.Result);
            // assert
            Assert.Equal(typeof(OkObjectResult), viewResult.GetType());
        }

        private List<AuthorDto> GetAuthorsData()
        {
            List<AuthorDto> authorsData = new List<AuthorDto>
            {
                new AuthorDto
                {
                    Id = 1,
                    FullName = "Author1"
                },
                new AuthorDto
                {
                    Id = 2,
                    FullName = "Author2"
                },
                new AuthorDto
                {
                    Id = 3,
                    FullName = "Author3"
                },
                new AuthorDto
                {
                    Id = 4,
                    FullName = "Author4"
                },
            };

            return authorsData;
        }
    }
}