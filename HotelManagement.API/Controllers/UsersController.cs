using HotelManagement.API.ViewModel;
using HotelManagement.Models;
using HotelManagement.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using Polly;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Declaring user service
        private readonly IUserService userService;
        IConfiguration configuration;
        private object _configuration;

        // Constructor for UsersController with dependency injection of userService
        public UsersController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }


        /// <summary>
        /// API Register method takes user properties and add user to the users table
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>user</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.ProfilePic))
            {
                vm.ProfilePic = "https://img.freepik.com/free-vector/mysterious-mafia-man-smoking-cigarette_52683-34828.jpg?size=338&ext=jpg&ga=GA1.2.1041511529.1663508133";
            }
            var user = new User()
            {

                Name = vm.Name,
                Email = vm.Email,
                Password = vm.Password,
                ProfilePic = vm.ProfilePic,
                PhoneNumber = vm.PhoneNumber,
                AadhaarId = vm.AadhaarId
                

            };

            await userService.AddUser(user);
            return Ok(new {Name= user.Name , Email=user.Email , ProfilePic = user.ProfilePic});
        }


        /// <summary>
        /// API Login method takes login info and invoking login method in user service
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns>user</returns>
        /// 
        [HttpPost("login")]
       // [ExceptionMapper(ExceptionType = typeof(InvalidCredentialsException), StatusCode = 401)]
        public async Task<IActionResult> Login([FromBody] LoginInfo loginInfo)
        {
            var user = await userService.Login(loginInfo.Email, loginInfo.Password);
            if (user == null)
                return BadRequest(new { message="Invalid Credentials !"});
           

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);



            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine("ok");
            Console.WriteLine(tokenString);
            return Ok(new
            {
                token = tokenString,
                user = user

            });
        }

        /// <summary>
        /// API getUser method takes user email and get user details from user service
        /// </summary>
        /// <param name="email"></param>
        /// <returns>user</returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> getUser(string email)
        { 
            var user = await userService.GetUserByEmail(email);

            return Ok(user);
        }
    }
}
