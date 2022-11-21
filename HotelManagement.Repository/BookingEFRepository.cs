using HotelManagement.Models;
using HotelManagement.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace HotelManagement.Repository
{

    /// <summary>
    /// creating repository which makes an abstraction 
    /// layer between the data access layer and the business 
    /// logic layer of an application.
    /// </summary>
 

    public class BookingEFRepository : IRepository<Booking, int>

    {
        private readonly DataBaseContext _context;
        private readonly ILogger<BookingEFRepository> _logger;

        /// <summary>
        /// the constructor calling object to pass in an instance of the context
        /// as dependency injection
        /// </summary>
        /// <param name="context"></param>
        public BookingEFRepository(DataBaseContext context, ILogger<BookingEFRepository> logger)
        {
            this._context = context;
            this._logger = logger;
           
        }

        
        /// <param name="entity"></param>
        /// <returns>it saves the entity in the services</returns>
        public async Task<Booking> Add(Booking entity)
        {
            _logger.LogInformation("Calling Add in BookingEF Repository");
            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <returns>list of bookings</returns>
        public async Task<List<Booking>> GetAll()
        {
            _logger.LogInformation("Calling GetAll in BookingEF Repository");
            await Task.CompletedTask;
            return _context.Bookings.ToList();
        }


        
        /// <param name="id">it saves email in the service and determine</param>
        /// <returns>booking info</returns>
        /// <exception cref="InvalidIdException">if the booking is not in the bookings table 
        /// exception will be thrown</exception>
       

        public async Task<Booking> GetById(int id)

        {
            _logger.LogInformation("Calling GetById in BookingEF Repository");
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            return booking ?? throw new InvalidIdException(id);
        }


        
        /// <returns>it contains no of entries written to the database</returns>
        

        public async Task Remove(int id)

        {
            _logger.LogInformation("Calling Remove in BookingEF Repository");
            var booking = await GetById(id);
            if(booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

       
      
        public async Task Save()
        {
            _logger.LogInformation("Calling Save in BookingEF Repository");
            await _context.SaveChangesAsync();
        }

        public async Task Update(Booking entity)
        {
            _logger.LogInformation("Calling update in BookingEF Repository");
            var oldBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == entity.Id);

             oldBooking.NumberOfDaysStay = entity.NumberOfDaysStay;
             oldBooking.Price = entity.Price;

            await Save();

        }
    }
}
