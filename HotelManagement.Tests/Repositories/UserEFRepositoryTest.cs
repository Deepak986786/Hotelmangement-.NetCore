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
    public class UserEFRepositoryTest : IDisposable
    {
        
        private DataBaseContext _context;
        private readonly UserEFRepository sut;  

        public UserEFRepositoryTest()
        {
            var logger = Mock.Of<ILogger<UserEFRepository>>();
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new DataBaseContext(options);
            _context.Database.EnsureCreated();
            _context.Users.AddRange(UserMockData.GetUsers());
            _context.SaveChanges(); 
            sut = new UserEFRepository(_context, logger);   
        }

        [Fact]
        public async Task GetAllReturnsAllUsers()
        {
            
           var results = await sut.GetAll();

           results.Should().HaveCount(UserMockData.GetUsers().Count);

        }

        [Fact]
        public async Task GetAllReturnsEmptyForEmptyUsers()
        {
            _context.Database.EnsureDeleted();
            var result=await sut.GetAll();

            result.Should().HaveCount(UserMockData.GetUsersEmpty().Count);
        }

        [Fact]
        public async Task GetByIdReturnsValidUser()
        {
            var user = UserMockData.GetUsers().First();
            var result = await sut.GetById(user.Email);

            result.Email.Should().BeSameAs(user.Email);
        }

        [Fact]
        public async Task AddAddstheUser()
        {
            var user=UserMockData.GetUser();

            await sut.Add(user);
            var result = await sut.GetAll();

            result.Should().HaveCount(UserMockData.GetUsers().Count + 1);
            
        }

        [Fact]
        public async Task RemoveRemovesUser()
        {
            var user = UserMockData.GetUsers().First();

            await sut.Remove(user.Email);

            await Assert.ThrowsAsync<InvalidIdException>(async () => await sut.GetById(user.Email));


        }

            



        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
