using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Authorization;
using UsersAPI.Data;
using UsersAPI.Models;
using UsersAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UsersAPI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connString = builder.Configuration["ConnectionStrings:UserConnection"];

            builder.Services.AddDbContext<UserDbContext>
                (opts =>
                {
                    opts.UseMySql(connString,
                        ServerVersion.AutoDetect(connString));
                }
                );

            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
                    ValidateAudience = false, 
                    ValidateIssuer = false, 
                    ClockSkew = TimeSpan.Zero
                };
            });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MinimumAge", policy
                    => policy.AddRequirements(new MinimumAge(18))
                );
            });

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TokenService>();

            builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
