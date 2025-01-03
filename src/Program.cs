﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.DataMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using NZWalks.Repository.Interfaces;
using NZWalks.Repository.Implementations;
using Serilog;


namespace NZWalks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();

            string log_file = @"Logs\app.log";

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(log_file, rollingInterval: RollingInterval.Minute)
                .MinimumLevel.Information()
                .CreateLogger();

            builder.Services.AddSerilog(logger);

            var records_connection_string = builder.Configuration.GetConnectionString("NZWlaksConnection");
            var auth_connection_string = builder.Configuration.GetConnectionString("NZWlaksAuthConnection");
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

            builder.Services.AddDbContext<NZWalksRecordsDbContext>(options => options.UseSqlServer(records_connection_string).EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information));
            builder.Services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlServer(auth_connection_string));

            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddScoped<IRegionRepository, RegionRepository>();
            builder.Services.AddScoped<IWalkRepository, WalkRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            // builder.Services.AddOpenApi();

           

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRateLimiter(rate_limit =>
            {
                rate_limit.AddFixedWindowLimiter("fixed", options =>
                {
                    options.QueueLimit = 0;
                    options.PermitLimit = 2;
                    options.Window = TimeSpan.FromSeconds(5);
                });

                rate_limit.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
                .AddEntityFrameworkStores<NZWalksAuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(security =>
            {
                security.Password.RequireDigit = false;
                security.Password.RequireLowercase = false;
                security.Password.RequireUppercase = false;
                security.Password.RequireNonAlphanumeric = false;
                security.Password.RequiredLength = 6;
                security.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRateLimiter();

            app.MapControllers();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/images"
            });

            app.Run();
        }
    }
}
