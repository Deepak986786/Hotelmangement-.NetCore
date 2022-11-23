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
        // Declaring instane of IRepository and ILogger.
        private IRepository<Booking, int> _bookingRepository;
        
        private readonly ILogger<BookingServiceV1> _logger;


        // Constructor with repository dependency injection.
        public BookingServiceV1(IRepository<Booking, int> bookingRepository,ILogger<BookingServiceV1> logger)

        {
            _bookingRepository = bookingRepository;
            _logger = logger;

        }


        /// <summary>
        /// This method calls the add method of repository
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>booking</returns>
        public async Task<Booking> AddBooking(Booking booking)
        {
            _logger.LogInformation("AddBooking called in Booking Service");
            await _bookingRepository.Add(booking);
            _logger.LogInformation("Returning booking from Booking Service");
            return booking;
        }

        /// <summary>
        /// This method calls the Remove method of repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteBooking(int id)
        {
            _logger.LogInformation("DeleteBooking called in Booking Service");
            await _bookingRepository.Remove(id);
            _logger.LogInformation("DeleteBooking method ended in Booking Service");
        }

        /// <summary>
        /// This method calls the getall method of booking repository
        /// </summary>
        /// <returns>list of bookings</returns>
        public async Task<List<Booking>> GetAllBookings()
        {
            _logger.LogInformation("Get all bookings called in Booking Service");
            var bookings =  await _bookingRepository.GetAll();

           return bookings;

        }

        /// <summary>
        /// This method calls getById method of repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>booking</returns>
        /// <exception cref="InvalidIdException"></exception>
        public async Task<Booking> GetBooking(int id)
        {
            _logger.LogInformation("get booking by id called in booking service");
            var booking = await _bookingRepository.GetById(id);
            if (booking!=null)
                return booking;
            throw new InvalidIdException(id);
        }


        /// <summary>
        /// This method calls the update method of repository
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task UpdateBooking(Booking booking)
        {
            await _bookingRepository.Update(booking);
        }
    }
}
