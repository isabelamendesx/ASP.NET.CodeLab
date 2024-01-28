using Microsoft.EntityFrameworkCore;
using PurplePizza_API.Data;
using PurplePizza_API.Services;

namespace PurplePizza_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Injetando PizzaService
            builder.Services.AddScoped<IPizzaService, PizzaService>();

            // Configurando SQLite
            builder.Services.AddDbContext<PizzaContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
