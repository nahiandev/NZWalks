using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.DataMapper;
using NZWalks.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NZWalks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var records_connection_string = builder.Configuration.GetConnectionString("NZWlaksConnection");
            var auth_connection_string = builder.Configuration.GetConnectionString("NZWlaksAuthConnection");
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

            builder.Services.AddDbContext<NZWalksRecordsDbContext>(options => options.UseSqlServer(records_connection_string));
            builder.Services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlServer(auth_connection_string));

            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddScoped<IRegionRepository, RegionRepository>();
            builder.Services.AddScoped<IWalkRepository, WalkRepository>();


            builder.Services.AddControllers();
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
            
            InitializeSwagger(app);

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRateLimiter();
            
            app.MapControllers();

            app.Run();
        }

        private static void InitializeSwagger(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
