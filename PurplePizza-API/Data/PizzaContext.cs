using Microsoft.EntityFrameworkCore;

namespace PurplePizza_API.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
    : base(options)
        {

        }
        public DbSet<Models.Pizza>? PizzaList { get; set; }
    }
}
