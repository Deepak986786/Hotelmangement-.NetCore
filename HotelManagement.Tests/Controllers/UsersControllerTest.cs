using FluentAssertions;
using HotelManagement.API.Controllers;
using HotelManagement.Models;
using HotelManagement.Services.UserService;
using HotelManagement.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.Controllers
{
    public class UsersControllerTest
    {
        private UsersController sut;
        private List<User> users;
        private Mock<IUserService> userService;
        private IConfiguration config;
        private ILogger<UsersController> logger;

        public UsersControllerTest()
        {
            userService = new Mock<IUserService>();
            config = Mock.Of<IConfiguration>();
            logger = Mock.Of<ILogger<UsersController>>();


            users = UserMockData.GetUsers();
        }

        [Fact]

        public async Task RegisterReturns200ForValidCase()
        {
            //Arrange
            var userModel = UserMockData.GetViewModel();
            var user = UserMockData.GetUser();
            userService.Setup(a => a.AddUser(user)).ReturnsAsync(user);
            sut = new UsersController(userService.Object, config, logger);
            //Act
            var result = (OkObjectResult)await sut.Register(userModel);

            //Assert
            result.StatusCode.Should().Be(200);


        }

    }
}
