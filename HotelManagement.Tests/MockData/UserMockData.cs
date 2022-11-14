﻿using HotelManagement.API.ViewModel;
using HotelManagement.Models;
using Microsoft.VisualStudio.Services.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.MockData
{
    public class UserMockData
    {
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Name="Anbu",
                    Email="anbu@gmail.com",
                    ProfilePic="",
                    PhoneNumber="12345",
                    AadhaarId="123456",
                    Password="pass"

                },
                new User
                {
                    Name="Anandh",
                    Email="anandh@gmail.com",
                    ProfilePic="",
                    PhoneNumber="12345",
                    AadhaarId="123456",
                    Password="pass"

                },
                new User
                {
                    Name="Gomz",
                    Email="gomz@gmail.com",
                    ProfilePic="",
                    PhoneNumber="12345",
                    AadhaarId="123456",
                    Password="pass"

                }
            };

            

        }
      

        public static LoginInfo NewLoginInfo()
        {
            

        }
    }
}