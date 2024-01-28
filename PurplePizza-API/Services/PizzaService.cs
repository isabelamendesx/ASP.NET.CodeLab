using PurplePizza_API.Models;
using PurplePizza_API.Data;

namespace PurplePizza_API.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaContext _context = default!;

        public PizzaService(PizzaContext context)
        {
            _context = context;
        }

        public IList<Pizza> GetAllPizzas()
        {
            if (_context.PizzaList != null)
            {
                return _context.PizzaList.ToList();
            }
            return new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            if (_context.PizzaList != null)
            {
                _context.PizzaList.Add(pizza);
                _context.SaveChanges();
            }
        }

        public void DeletePizza(Pizza pizza)
        {
            if (_context.PizzaList != null)
            {
                _context.PizzaList.Remove(pizza);
                _context.SaveChanges();
            }
        }

        public void UpdatePizza(Pizza pizzaToUpdate, Pizza updatedPizza)
        {

            if (pizzaToUpdate != null)
            {
                pizzaToUpdate.Name = updatedPizza.Name;
                pizzaToUpdate.IsGlutenFree = updatedPizza.IsGlutenFree;
                _context.SaveChanges();
            }

        }
    }
}
