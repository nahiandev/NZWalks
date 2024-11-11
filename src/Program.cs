using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.DataMapper;
using NZWalks.Repository;

namespace NZWalks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection_string = builder.Configuration.GetConnectionString("NZWlaksConnection");
            builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(connection_string));
            
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

            

            var app = builder.Build();
            
            InitializeSwagger(app);

            app.UseHttpsRedirection();

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
