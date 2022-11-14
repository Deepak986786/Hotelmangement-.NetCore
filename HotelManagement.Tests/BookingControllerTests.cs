using HotelManagement.API.Controllers;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Services.BookingService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests
{
    public class BookingControllerTests
    {
        private readonly BookingsController bookingsController;
        
        public BookingControllerTests(BookingsController bookingsController)
        {
            this.bookingsController = bookingsController;
        }
        [Fact]
       /* public async Task GetAllBookingsReturnsAllBookings()
        {
            // Arrange
            var bookingService = new Mock<BookingServiceV1>();
            var bookingRepository = new Mock<BookingEFRepository>();
            *//*bookingService.Setup(b=>b.GetAllBookings())
                .Returns(Task.FromResult((new Booking { })))*//*
             
            // Act
             var result = await bookingsController.GetBookings();

          

            // Assert
            Assert.NotNull(result);
            // Assert.Equal(200, (result as IStatusCodeActionResult).StatusCode);
        }*/



        public async Task GetAllBookingsReturnsAllBookings()
        {
            // Arrange
           // var userData = A.Fake<UserEFRepository>();
           
            // Act
            var result = await bookingsController.GetBookings();



            // Assert
            Assert.NotNull(result);
            // Assert.Equal(200, (result as IStatusCodeActionResult).StatusCode);
        }
    }
}
