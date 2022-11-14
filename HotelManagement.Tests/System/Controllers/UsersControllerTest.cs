using HotelManagement.API.Controllers;
using HotelManagement.Models;
using HotelManagement.Services.UserService;
using HotelManagement.Tests.MockData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.System.Controllers
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
            userService =new Mock<IUserService>();
            config = Mock.Of<IConfiguration>();
            logger = Mock.Of<ILogger<UsersController>>();

            
            users = UserMockData.GetUsers();
        }

        [Fact]
        public async Task 

    }
}
