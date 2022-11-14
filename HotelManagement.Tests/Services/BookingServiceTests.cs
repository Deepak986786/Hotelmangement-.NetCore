using FluentAssertions;
using HotelManagement.API.Controllers;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Services.BookingService;
using HotelManagement.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// The below test method tests the getbookings method of booking service 
        /// by mocking the booking repository
        /// </summary>
 
        [Fact]
        public async Task GetAllBookings_ReturnAllBookings()
        {
            // Arrange
            var bookingRepository = new Mock<IRepository<Booking,int>>();
            bookingRepository.Setup(b => b.GetAll())
                    .Returns(Task.FromResult(BookingsMockData.GetBookings()));
            var sut = new BookingServiceV1(bookingRepository.Object);


            // Act
            var result = sut.GetAllBookings().Result;

            // Assert
            Assert.Equal(3, result.Count);
        }
        

 
     
    
    }
}
