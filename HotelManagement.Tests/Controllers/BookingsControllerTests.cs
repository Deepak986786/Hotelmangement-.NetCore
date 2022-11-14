using FluentAssertions;
using HotelManagement.API.Controllers;
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
    public class BookingsControllerTests
    {
        /// <summary>
        /// The test method name include methodname+testing scenario name.
        /// This test calls the getallbookings of controller by mocking the booking service
        /// </summary>
        private BookingsController sut;
        private Mock<IBookingService> bookingService;
        private IConfiguration _configuration;
        private ILogger<BookingsController> _logger;
     
        public BookingsControllerTests()
        {
            bookingService = new Mock<IBookingService>();
            _configuration = Mock.Of<IConfiguration>();
            _logger = Mock.Of<ILogger<BookingsController>>();
        }

        [Fact]
        public async Task GetAllBookings_ShouldReturn200Status()
        {
            // Arrange
           
            bookingService.Setup(x => x.GetAllBookings())
                .Returns(Task.FromResult(BookingsMockData.GetBookings()));
            // sut - system under test is recommended naming convention 
            sut = new BookingsController(bookingService.Object,_configuration,_logger);

            // Act
            var result = (OkObjectResult) await sut.GetBookings();


            // Assert
            Assert.Equal(200, result.StatusCode);
            
        }
        

        [Fact]
        public async Task GetAllBookings_Should_Return204NoContentStatusForEmptyData()
        {
            // Arrange
            
            bookingService.Setup(b => b.GetAllBookings())
                .Returns(Task.FromResult(BookingsMockData.GetEmptyBookings()));
            sut = new BookingsController(bookingService.Object,_configuration,_logger);

            // Act
            var result = (NoContentResult)await sut.GetBookings();

            // Assert
            Assert.Equal(204, result.StatusCode);
            bookingService.Verify(_ => _.GetAllBookings(), Times.Exactly(1));
        }

        [Fact]
        public async Task AddBooking_Should_Return201ForSuccessfulBooking()
        {
            // Arrange

            var booking = BookingsMockData.GetBookings().First();
            var bookingModel = BookingsMockData.GetBookingVms().First();

            bookingService.Setup(b => b.GetAllBookings()).ReturnsAsync(BookingsMockData.GetBookings());
            bookingService.Setup(b => b.AddBooking(booking))
                .ReturnsAsync(booking);
            // sut - system under test is recommended naming convention 
             sut = new BookingsController(bookingService.Object, _configuration, _logger);

            // Act
            var result = (CreatedResult)await sut.Create(bookingModel);


            // Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task AddBooking_Should_Return400WhenRoomsAreNotAvailable()
        {
            // Arrange

            var booking = BookingsMockData.GetBookings().First();
            var bookingModel = BookingsMockData.GetBookingVms().First();

            bookingService.Setup(b => b.GetAllBookings()).ReturnsAsync(BookingsMockData.GetBookings());
            bookingService.Setup(b => b.AddBooking(booking))
                .ReturnsAsync(booking);
            // sut - system under test is recommended naming convention 
            sut = new BookingsController(bookingService.Object, _configuration, _logger);

            // Act
            var result = (CreatedResult)await sut.Create(bookingModel);


            // Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task DeleteBooking_Should_Return204NoContentStatusOnSuccessfulDeletion()
        {
            // Arrange
            var bookingId = BookingsMockData.GetBookings().First().Id;
            bookingService.Setup(b => b.DeleteBooking(bookingId));
            sut = new BookingsController(bookingService.Object, _configuration, _logger);

            // Act
            var result = (NoContentResult)await sut.DeleteBooking(bookingId);

            // Assert
            Assert.Equal(204, result.StatusCode);

        }
        [Fact]
        public async Task UpdateBooking_Should_Return202OnSuccessfulUpdation()
        {
            // Arrange
            //var booking = BookingsMockData.GetBookings().First();
            var booking = BookingsMockData.GetBooking(1);
            var bookingViewModel = BookingsMockData.GetBookingVms().First();

            bookingService.Setup(b => b.GetBooking(1)).ReturnsAsync(booking);

            bookingService.Setup(b => b.UpdateBooking(booking));
            sut = new BookingsController(bookingService.Object, _configuration, _logger);

            // Act
            var result = (AcceptedResult)await sut.UpdateBooking(bookingViewModel,booking.Id);

            // Assert
            Assert.Equal(202, result.StatusCode);

        }
        [Fact]
        public async Task UpdateBooking_Should_Return400ForInvalidBookingId()
        {
            // Arrange
            var booking = BookingsMockData.GetBookings().First();
            var bookingViewModel = BookingsMockData.GetBookingVms().First();

            bookingService.Setup(b => b.UpdateBooking(booking));
            sut = new BookingsController(bookingService.Object, _configuration, _logger);

            // Act
            var result = (BadRequestResult)await sut.UpdateBooking(bookingViewModel, 1000);

            // Assert
            Assert.Equal(400, result.StatusCode);

        }

    }
}
