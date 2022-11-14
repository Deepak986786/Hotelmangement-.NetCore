using Castle.Core.Logging;
using HotelManagement.API.Controllers;
using HotelManagement.API.ViewModel;
using HotelManagement.Models;
using HotelManagement.Services.BookingService;
using HotelManagement.Services.UserService;
using HotelManagement.Tests.MockData;
using log4net.Repository.Hierarchy;
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
    public class UsersControllerTests
    {
        private IConfiguration _configuration;
        private ILogger<UsersController> _logger;
        public UsersControllerTests()
        {
            _configuration = Mock.Of<IConfiguration>();
            _logger = Mock.Of<ILogger<UsersController>>();
        }

        [Fact]
        public async Task GetUser_ShouldReturn200StatusForRegisteredUser()
        {
            // Arrange
            var user = UsersMockData.GetAllUsersInfo().First();
            var userService = new Mock<IUserService>();
            // string email = "srilakshmi27272@gmail.com";
            userService.Setup(x => x.GetUserByEmail(user.Email))
                .Returns(Task.FromResult(UsersMockData.GetUserInfo()));
                
            // sut - system under test is recommended naming convention 
            var sut = new UsersController(userService.Object,_configuration,_logger);

            // Act
            var result = (OkObjectResult)await sut.GetUser(user.Email);


            // Assert
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public async Task GetAllUsers_ShouldReturn200StatusForRegisteredUser()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetAllUsers())
                .ReturnsAsync(UsersMockData.GetAllUsers());
            // sut - system under test is recommended naming convention 
            var sut = new UsersController(userService.Object, _configuration, _logger);
            // Act
            var result = (OkObjectResult)await sut.GetAllUsers();


            // Assert
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public async Task GetAllUsers_Should_Return204NoContentStatusForEmptyData()
        {
         
            // Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetAllUsers())
                .ReturnsAsync(UsersMockData.GetEmptyUsers());
            // sut - system under test is recommended naming convention 
            var sut = new UsersController(userService.Object, _configuration, _logger);
            // Act
            var result = (NoContentResult)await sut.GetAllUsers();
            // Assert
            Assert.Equal(204, result.StatusCode);
            userService.Verify(_ => _.GetAllUsers(), Times.Exactly(1));
        }
        [Fact]
        public async Task Register_ShouldReturn200ForSuccessfulRegistration()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var user = UsersMockData.GetAllUsers().First();
            
            userService.Setup(x => x.AddUser(user))
                .Returns(Task.FromResult(UsersMockData.Register(user)));
            // sut - system under test is recommended naming convention 
            var sut = new UsersController(userService.Object,_configuration,_logger);

            // Act
            var result = (OkObjectResult)await sut.Register(UsersMockData.GetAllUsersViewModels().First());


            // Assert
            Assert.Equal(200, result.StatusCode);

        }

    }
}
