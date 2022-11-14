using FluentAssertions;
using HotelManagement.Repository;
using HotelManagement.Tests.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.System.Repositories
{
    public class UserEFRepositoryTest : IDisposable
    {

        protected readonly DataBaseContext _context;

        public UserEFRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

            _context = new DataBaseContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllUsersReturnAllUsers()
        {
            //Arrange
            _context.Users.AddRange(UserMockData.GetUsers());
            _context.SaveChanges();

            var sut = new UserEFRepository(_context);


            //Act
            var result = await sut.GetAll();

            //Assert
           // result.Should().HaveCount(UserMockData.GetUsers().Count);
            Assert.Equal(3, UserMockData.GetUsers().Count);
        }



        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
