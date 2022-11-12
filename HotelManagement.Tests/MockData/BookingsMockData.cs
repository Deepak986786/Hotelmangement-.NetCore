using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.MockData
{
    public class BookingsMockData
    {
        public static List<Booking> GetBookings()
        {
            return new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    UserId = "srilakshmi27272@gmail.com",
                    RoomNo = 1,
                    NumberOfDaysStay = 1,
                    Price = 500
                },
                new Booking {
                    Id = 2,
                    UserId = "santhosh@gmail.com",
                    RoomNo = 2,
                    NumberOfDaysStay = 3,
                    Price = 1500

                },
                new Booking {
                    Id = 3,
                    UserId = "eswaree@gmail.com",
                    RoomNo = 3,
                    NumberOfDaysStay = 2,
                    Price = 1000

                }


        };
        }

        public static List<Booking> GetEmptyBookings()
        {
            return new List<Booking>();
        }
    }
}