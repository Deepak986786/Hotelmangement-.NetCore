﻿using HotelManagement.Models;
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
           
                    NumberOfDaysStay = 1,
                    Price = 500,
                    BookingDate = DateTime.Today
                },
                new Booking {
                    Id = 2,
                    UserId = "santhosh@gmail.com",
                    NumberOfDaysStay = 3,
                    Price = 1500,
                    BookingDate = DateTime.Today

                },
                new Booking {
                    Id = 3,
                    UserId = "eswaree@gmail.com",
                    NumberOfDaysStay = 2,
                    Price = 1000,
                    BookingDate = DateTime.Today

                }


        };
        }

        public static Booking GetBooking()
        {
            return new Booking
            {
                Id = 4,
                UserId = "santhosh1@gmail.com",
                NumberOfDaysStay = 3,
                Price = 1500,
                BookingDate = DateTime.Today

            };
        }

        public static List<Booking> GetEmptyBookings()
        {
            return new List<Booking>();
        }
        
    }
}