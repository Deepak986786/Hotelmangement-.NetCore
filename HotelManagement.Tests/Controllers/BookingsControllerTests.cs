using HotelManagement.API.Controllers;
using HotelManagement.Services.BookingService;
using HotelManagement.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
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
        [Fact]
        public async Task GetAllBookings_Should_Return200Status()
        {
            // Arrange
            var bookingService = new Mock<IBookingService>();
            bookingService.Setup(x => x.GetAllBookings())
                .Returns(Task.FromResult(BookingsMockData.GetBookings()));
            var bookingController = new BookingsController(bookingService.Object);

            // Act
            var result = (OkObjectResult) await bookingController.GetBookings();


            // Assert
            Assert.Equal(200, result.StatusCode);
            
        }

        [Fact]
        public async Task GetAllBookings_Should_Return204NoContentStatus()
        {
            // Arrange
            var bookingService = new Mock<IBookingService>();
            bookingService.Setup(b => b.GetAllBookings())
                .Returns(Task.FromResult(BookingsMockData.GetEmptyBookings()));
            var bookingController = new BookingsController(bookingService.Object);

            // Act
            var result = (NoContentResult)await bookingController.GetBookings();

            // Assert
            Assert.Equal(204, result.StatusCode);
            bookingService.Verify(_ => _.GetAllBookings(), Times.Exactly(1));
        }
    }
}
