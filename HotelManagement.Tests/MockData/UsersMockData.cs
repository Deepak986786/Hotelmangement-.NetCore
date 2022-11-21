using HotelManagement.API.ViewModel;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Tests.MockData
{
    public class UsersMockData
    {
        public static List<User> GetAllUsers()
        {
            return new List<User> {
            new User()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                Password="1234",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                PhoneNumber = "9164371293",
                AadhaarId = "973141225798",
                UserBookings=new List<Booking>()
            },
            new User()
            {
                Name = "Santhosh Kumar",
                Email = "santohsh@gmail.com",
                Password="1234",
                ProfilePic = "https://randomuser.me/api/portraits/men/14.jpg",
                PhoneNumber = "9133792145",
                AadhaarId = "234812347546",
                UserBookings=new List<Booking>()
            }
            };
        }

        public static List<User> GetEmptyUsers()
        {
            return new List<User>();
        }
        public static List<UserViewModel> GetAllUsersViewModels()
        {
            return new List<UserViewModel> {
            new UserViewModel()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                Password="1234",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                PhoneNumber = "9164371293",
                AadhaarId = "973141225798",
              
            },
            new UserViewModel()
            {
                Name = "Santhosh Kumar",
                Email = "santohsh@gmail.com",
                Password="1234",
                ProfilePic = "https://randomuser.me/api/portraits/men/14.jpg",
                PhoneNumber = "9133792145",
                AadhaarId = "234812347546",
               
            }
            };
        }
        public static List<UserInfo> GetAllUsersInfo()
        {
            return new List<UserInfo> {
            new User()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                PhoneNumber = "9164371293",
                AadhaarId = "973141225798"
            },
            new User()
            {
                Name = "Santhosh Kumar",
                Email = "santohsh@gmail.com",
                ProfilePic = "https://randomuser.me/api/portraits/men/14.jpg",
                PhoneNumber = "9133792145",
                AadhaarId = "234812347546"
            }
            };
        }
        public static User GetUserInfo()
        {
            return new User
            {
                Name = "Santhosh Kumar",
                Email = "santohsh@gmail.com",
                ProfilePic = "https://randomuser.me/api/portraits/men/14.jpg",
                PhoneNumber = "9133792145",
                AadhaarId = "234812347546"
            };   
        }
       
        public static User Register(User user)
        {
            return new User()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                Password = "1234",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                PhoneNumber = "9164371293",
                AadhaarId = "973141225798",
                UserBookings = new List<Booking>()
            };
        }
        public static User GetUser()
        {
            return new User()
            {
                Name = "Divya",
                Email = "divya@gmail.com",
                Password="1234",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                PhoneNumber = "91643713293",
                AadhaarId = "453141225798",
                UserBookings = new List<Booking>()
            };
        }
    }
    }


