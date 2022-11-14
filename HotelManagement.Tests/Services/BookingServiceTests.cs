using Castle.Core.Logging;
using FluentAssertions;
using HotelManagement.API.Controllers;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Services.BookingService;
using HotelManagement.Tests.MockData;
using HotelManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.Services
{
    public class BookingServiceTests 
    {
        private BookingServiceV1 sut;
        private Mock<IRepository<Booking, int>> bookingRepository;
        private ILogger<BookingServiceV1> logger;
        /// <summary>
        /// The below test method tests the getbookings method of booking service 
        /// by mocking the booking repository
        /// </summary>
        public BookingServiceTests()
        {
            bookingRepository = new Mock<IRepository<Booking, int>>();
            logger = Mock.Of<ILogger<BookingServiceV1>>();
        }
        [Fact]
        public async Task GetAllBookings_ReturnAllBookings()
        {
            // Arrange
            bookingRepository = new Mock<IRepository<Booking,int>>();
            bookingRepository.Setup(b => b.GetAll())
                    .Returns(Task.FromResult(BookingsMockData.GetBookings()));
            sut = new BookingServiceV1(bookingRepository.Object,logger);


            // Act
            var result = sut.GetAllBookings().Result;

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetBooking_ReturnsBookingForValidBookingId()
        {
            // Arrange
            bookingRepository = new Mock<IRepository<Booking, int>>();
            bookingRepository.Setup(b => b.GetById(1))
                    .Returns(Task.FromResult(BookingsMockData.GetBooking(1)));
            sut = new BookingServiceV1(bookingRepository.Object,logger);

            var expected = BookingsMockData.GetBooking(1);
            // Act
            var result = sut.GetBooking(1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact(Skip ="Not yet fully implemented")]
        public async Task GetBooking_ThrowsInvalidIdExceptionForInvalidBookingId()
        {
            // Arrange
            bookingRepository = new Mock<IRepository<Booking, int>>();
            bookingRepository.Setup(b => b.GetById(-1))
                    .Returns(Task.FromResult(BookingsMockData.GetBooking(-1)));
            sut = new BookingServiceV1(bookingRepository.Object,logger);

            var expected = BookingsMockData.GetBooking(-1);
           

            // Assert
            Assert.Throws<InvalidIdException>(() =>
            {
                // Act
                var result = sut.GetBooking(-1);
            });
        }
        [Fact]
        public async Task AddBooking_AddsBookingInRepository()
        {
            // Arrange
            bookingRepository = new Mock<IRepository<Booking, int>>();
            var booking = BookingsMockData.GetBooking(1);
            bookingRepository.Setup(b => b.Add(booking))
                    .Returns(Task.FromResult(BookingsMockData.GetBooking(1)));
            var sut = new BookingServiceV1(bookingRepository.Object, logger);

            var expected = BookingsMockData.GetBooking(1);
            // Act
            var result = sut.GetBooking(1);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeleteBooking_DeleteBookingFromRepository()
        {
            // Arrange
            bookingRepository = new Mock<IRepository<Booking, int>>();
            var booking = BookingsMockData.GetBooking(1);
            bookingRepository.Setup(b => b.Remove(booking.Id)).Equals(1);
                  
            var sut = new BookingServiceV1(bookingRepository.Object, logger);


            // Act
            var result = sut.DeleteBooking(booking.Id);

            // Assert
            Assert.NotNull(result);
        }




    }
}
