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
        private BookingsController sut;
        private Mock<IBookingService> bookingService;
        public BookingsControllerTests()
        {
            bookingService = new Mock<IBookingService>();
        }

        [Fact]
        public async Task GetBookingsReturns200ForValidCase()
        {
            //Arrange
            var bookings = BookingsMockData.GetBookings();
            bookingService.Setup(a => a.GetAllBookings()).ReturnsAsync(bookings);
            sut = new BookingsController(bookingService.Object);

            //Act
            var result=(OkObjectResult) await sut.GetBookings();

            //Assert

            Assert.Equal(200, result.StatusCode);

        }

    }
}
