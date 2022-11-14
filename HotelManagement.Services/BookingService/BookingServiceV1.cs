using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Services.UserService;
using HotelManagement.Utils;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.BookingService
{
    public  class BookingServiceV1  : IBookingService
    {
        private IRepository<Booking, int> _bookingRepository;
        // Declaring instane of ILogger.
        private readonly ILogger<BookingServiceV1> logger;


        // Constructor with repository dependency injection
        public BookingServiceV1(IRepository<Booking, int> bookingRepository,ILogger<BookingServiceV1> logger)

        {
            _bookingRepository = bookingRepository;
            this.logger = logger;

        }


        /// <summary>
        /// this calls the repository of add method
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>booking</returns>
        public async Task<Booking> AddBooking(Booking booking)
        {
            logger.LogInformation("AddBooking called in Booking Service");
            await _bookingRepository.Add(booking);
            logger.LogInformation("Returning booking from Booking Service");
            return booking;
        }

        public async Task DeleteBooking(int id)
        {
            logger.LogInformation("DeleteBooking called in Booking Service");
            await _bookingRepository.Remove(id);
            logger.LogInformation("DeleteBooking method ended in Booking Service");
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            logger.LogInformation("Get all bookings called in Booking Service");
            var bookings =  await _bookingRepository.GetAll();

           return bookings;

        }

        public async Task<Booking> GetBooking(int id)
        {
            logger.LogInformation("get booking by id called in booking service");
            var booking = await _bookingRepository.GetById(id);
            if (booking!=null)
                return booking;
            throw new InvalidIdException(id);
        }



        public async Task UpdateBooking(Booking booking)
        {
            await _bookingRepository.Update(booking);
        }
    }
}
