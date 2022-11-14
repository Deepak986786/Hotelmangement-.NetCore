using HotelManagement.API.Controllers;
using HotelManagement.Repository;
using HotelManagement.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly UserServiceV1 userService;
        private readonly UserEFRepository repository;

        /*public UsersControllerTests(UsersController controller 
            //UserServiceV1 userService,
            //UserEFRepository repository
            )
        {
            _controller = controller;
            *//*this.userService = userService;
            this.repository = repository;*//*
        }*/
        [Fact]
        public async Task GetUserReturnsUserObject()
        {
            // Arrange
            var userRepository = new Mock<UserEFRepository>();
            var userService = new Mock<UserServiceV1>();
            string email = "srilakshmi27272@gmail.com";
           // userService.Setup(x => x.GetUserByEmail(email))

            // Act
            var result = await _controller.getUser(email);
            // Assert
            //Assert.IsType<OkObjectResult>(result as OkObjectResult);
            Assert.Equal(200, (result as IStatusCodeActionResult).StatusCode);
        }
    }
}
