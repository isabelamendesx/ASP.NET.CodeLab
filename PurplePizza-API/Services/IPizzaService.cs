using PurplePizza_API.Models;

namespace PurplePizza_API.Services
{
    public interface IPizzaService
    {
        IList<Pizza> GetAllPizzas();
        void AddPizza(Pizza pizza);
        void DeletePizza(Pizza pizza);
        void UpdatePizza(Pizza pizzaToUpdate, Pizza updatedPizza);
    }
}
