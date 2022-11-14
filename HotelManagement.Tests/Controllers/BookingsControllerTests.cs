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
        /// <summary>
        /// The test method name include methodname+testing scenario name.
        /// This test calls the getallbookings of controller by mocking the booking service
        /// </summary>
       
        [Fact]
        public async Task GetAllBookings_ShouldReturn200Status()
        {
            // Arrange
            var bookingService = new Mock<IBookingService>();
            bookingService.Setup(x => x.GetAllBookings())
                .Returns(Task.FromResult(BookingsMockData.GetBookings()));
            // sut - system under test is recommended naming convention 
            var sut = new BookingsController(bookingService.Object);

            // Act
            var result = (OkObjectResult) await sut.GetBookings();


            // Assert
            Assert.Equal(200, result.StatusCode);
            
        }
        

        [Fact]
        public async Task GetAllBookings_Should_Return204NoContentStatusForEmptyData()
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
