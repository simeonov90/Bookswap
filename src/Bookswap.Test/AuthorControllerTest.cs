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
        public void ShouldGetAuthorsList()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.GetAllAsync().Result)
                .Returns(authors);
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorsResult = authorController.GetAuthors().Result;

            // assert
            Assert.NotNull(authorsResult);
        }

        [Fact]
        public void ShouldGetAuthorById()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.GetById(1).Result)
                .Returns(authors[0]);
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorResult = authorController.GetAuthorById(1);

            // assert
            Assert.NotNull(authorResult);
            Assert.Equal(authors[0].Id, authorResult.Id);
        }

        [Fact]
        public void ShouldGetAuthorByIdReturnNotFound()
        {
            // arrange
            var authors = GetAuthorsData();
            authorServiceMock.Setup(a => a.GetById(0).Result);
            var logger = new Mock<ILogger<AuthorController>>();
            var authorController = new AuthorController(authorServiceMock.Object, logger.Object);

            // act
            var authorResult = authorController.GetAuthorById(0).Result;
            var viewResult = Assert.IsType<NotFoundResult>(authorResult.Result);

            // assert
            Assert.Equal(typeof(NotFoundResult), viewResult.GetType());
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