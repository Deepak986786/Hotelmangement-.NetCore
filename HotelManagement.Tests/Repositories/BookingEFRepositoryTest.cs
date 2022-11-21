using FluentAssertions;
using HotelManagement.Repository;
using HotelManagement.Tests.MockData;
using HotelManagement.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.Repositories
{
    public class BookingEFRepositoryTest : IDisposable
    {
        private DataBaseContext _context;
        private readonly BookingEFRepository sut;

        public BookingEFRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new DataBaseContext(options);
            _context.Database.EnsureCreated();
            _context.Bookings.AddRange(BookingsMockData.GetBookings());
            _context.SaveChanges();
            sut = new BookingEFRepository(_context);
        }

        [Fact]
        public async Task AddAddsBookings()
        {
            //Arrange
            var booking = BookingsMockData.GetBooking();
            //Act
            await sut.Add(booking);
            var result=await sut.GetAll();
            //Assert
            result.Should().HaveCount(BookingsMockData.GetBookings().Count() + 1);

        }

        [Fact]
        public async Task GetAllReturnsAllBookings()
        {
            //Arrange
            var bookings = BookingsMockData.GetBookings();
            //Act
            var result = await sut.GetAll();
            //Assert
            result.Should().HaveCount(bookings.Count());
        }

        [Fact]
        public async Task GetByIdReturnsUser()
        {
            //Arrange
            var booking = BookingsMockData.GetBookings().First();

            //Act
            var result = await sut.GetById(booking.Id);

            //Assert
            result.Id.Should().Be(booking.Id);

        }

        [Fact]
        public async Task GetByIdReturnsExceptionForInvalidId()
        {
            //Arrange
            //var booking=BookingsMockData.GetBookings().First();
            //Act
            //var result = await sut.GetById(5);

            //Assert

            await Assert.ThrowsAsync<InvalidIdException>(async() => await sut.GetById(5));
        }

        [Fact]
        public async Task RemoveRemovesTheUser()
        {
            //Arrange
            var booking = BookingsMockData.GetBookings().First();

            //Act
            await sut.Remove(booking.Id);

            //Assert
            await Assert.ThrowsAsync<InvalidIdException>(async () => await sut.GetById(booking.Id));
        }

        [Fact]
        public async Task UpdateUpdatesTheUser()
        {
            //Arrange
            var booking = BookingsMockData.GetBookings().First();
            booking.Price = 1000;

            //Act
            await sut.Update(booking);
            var result=await sut.GetById(booking.Id);

            //Assert
            result.Price.Should().Be(booking.Price);

        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }
    }
}
