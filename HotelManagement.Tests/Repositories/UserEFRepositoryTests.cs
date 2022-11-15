using FluentAssertions;
using HotelManagement.Repository;
using HotelManagement.Tests.MockData;
using HotelManagement.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.Repositories
{
    public class UserEFRepositoryTests:IDisposable
    {
        private DataBaseContext _context;
        private readonly UserEFRepository sut;

        public UserEFRepositoryTests()
        {
            var logger = Mock.Of<ILogger<UserEFRepository>>();
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new DataBaseContext(options);
            _context.Database.EnsureCreated();
            _context.Users.AddRange(UsersMockData.GetAllUsers());
            _context.SaveChanges();
            sut = new UserEFRepository(_context, logger);
        }


        [Fact]
        public async Task GetAll_ReturnsAllUsers()
        {

            var results = await sut.GetAll();
            results.Should().HaveCount(UsersMockData.GetAllUsers().Count);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyForEmptyUsers()
        {
            _context.Database.EnsureDeleted();
            var result = await sut.GetAll();
            result.Should().HaveCount(UsersMockData.GetEmptyUsers().Count);
        }



        [Fact]
        public async Task GetById_ReturnsValidUser()
        {
            var user = UsersMockData.GetAllUsers().First();
            var result = await sut.GetById(user.Email);
            result.Email.Should().BeSameAs(user.Email);
        }
        [Fact]
        public async Task GetById_ThrowsExceptionForInvalidMail()
        {
            var user = UsersMockData.GetAllUsers().First();
           
            Assert.ThrowsAsync<InvalidIdException>(async () =>
            {
                var result = await sut.GetById(user.Email + "aa");
                
            });
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

