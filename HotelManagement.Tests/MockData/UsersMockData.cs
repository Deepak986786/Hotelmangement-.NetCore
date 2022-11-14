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
        public static List<User> users = new List<User>()
        {
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
        public static User GetUser(string email)
        {
            for(int i = 0; i < users.Count; i++)
            {
                if (users[i].Email == email)
                    return users[i];
            }
            return new User();
        }
       
        public static User Register(User user)
        {
            return new User()
            {
                Name = "Srilakshmi",
                Email = "srilakshmi27272@gmail.com",
                ProfilePic = "https://randomuser.me/api/portraits/women/63.jpg",
                PhoneNumber = "9164371293",
                AadhaarId = "973141225798"
            };
        }
        }
    }


