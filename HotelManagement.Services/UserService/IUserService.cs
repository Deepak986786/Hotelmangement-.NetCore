﻿using HotelManagement.Models;

namespace HotelManagement.Services.UserService
{
    public interface IUserService
    {
        public Task<User> AddUser(User user);

        public Task DeleteUser(string email);

        public Task<User> GetUserByEmail(string email);
        Task<User> Login(string email, string password);

        public Task<List<User>> GetAllUsers();
    }
}