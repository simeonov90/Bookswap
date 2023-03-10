using Bookswap.API.Controllers;
using Bookswap.Application.Services.Authors;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Test
{
    public class AuthorControllerTest
    {
        private readonly Mock<IAuthorService> authorServiceMock;
        private readonly Mock<ILogger<AuthorController>> logger;
        private readonly AuthorController authorController;
        public AuthorControllerTest()
        {
            this.authorServiceMock = new Mock<IAuthorService>();
            this.logger = new Mock<ILogger<AuthorController>>();
            this.authorController = new AuthorController(this.authorServiceMock.Object, this.logger.Object);
        }

        [Fact]
        public async Task ShouldGetAuthors()
        {
            var result = await this.authorController.GetAuthors();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ShouldGetCreateAuthor()
        {
            var result = await this.authorController.Create(new CreateAuthorDto { FullName = "Ivanov"});
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
