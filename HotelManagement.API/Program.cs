using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Services.BookingService;
using HotelManagement.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace HotelManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                            .AddControllers()
                            .AddJsonOptions(opt =>
                            {
                                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());


                            });

            // Adding JWt services Here

            builder
        .Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {

            opt.RequireHttpsMetadata = false;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,

                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };



        });


            // Adding Database context here

            builder.Services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("HotelManagementEF"));
            });

            // Adding Services related to User.
            builder.Services.AddTransient<IUserService, UserServiceV1>();
            builder.Services.AddTransient<IRepository<User, string>,UserEFRepository>();

            // Adding Services related to Booking
            builder.Services.AddTransient<IBookingService, BookingServiceV1>();
            builder.Services.AddTransient<IRepository<Booking,string>,BookingEFRepository>();


            //add repository and services to the Service Collection
            builder.Services.AddTransient<IUserService, UserServiceV1>();
            builder.Services.AddTransient<IRepository<User, string>,UserEFRepository>();


            builder.Services.AddTransient<IBookingService, BookingServiceV1>();
            builder.Services.AddTransient<IRepository<Booking, int>,BookingEFRepository>();

           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}