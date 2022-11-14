using Castle.Core.Logging;
using HotelManagement.API.Controllers;
using HotelManagement.API.ViewModel;
using HotelManagement.Models;
using HotelManagement.Services.BookingService;
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
    public class UsersControllerTests
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsersController> _logger;
        
        [Fact]
        public async Task GetUser_ShouldReturn200StatusForRegisteredUser()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            string email = "srilakshmi27272@gmail.com";
            userService.Setup(x => x.GetUserByEmail(email))
                .Returns(Task.FromResult(UsersMockData.GetUser(email)));
            // sut - system under test is recommended naming convention 
            var sut = new UsersController(userService.Object,_configuration,_logger);

            // Act
            var result = (OkObjectResult)await sut.getUser(email);


            // Assert
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public async Task Register_ShouldReturn200ForSuccessfulRegistration()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var user = new User()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                Password = "1234",
                PhoneNumber = "9164371293",
                AadhaarId = "973141225798"
            };
            var userViewModel = new UserViewModel()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",

                PhoneNumber = "9164371293",
                AadhaarId = "973141225798"
            };
            userService.Setup(x => x.AddUser(user))
                .Returns(Task.FromResult(UsersMockData.Register(user)));
            // sut - system under test is recommended naming convention 
            var sut = new UsersController(userService.Object,_configuration,_logger);

            // Act
            var result = (OkResult)await sut.Register(userViewModel);


            // Assert
            Assert.Equal(200, result.StatusCode);

        }

    }
}
