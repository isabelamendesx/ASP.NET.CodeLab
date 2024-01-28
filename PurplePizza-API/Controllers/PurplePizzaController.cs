using Microsoft.AspNetCore.Mvc;
using PurplePizza_API.Services;
using PurplePizza_API.Models;

namespace PurplePizza_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurplePizzaController : ControllerBase
    {
        private readonly IPizzaService _service;

        public PurplePizzaController(IPizzaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPizzas()
        {
            var pizzas = _service.GetAllPizzas();

            if (pizzas.Count > 0)
            {
                return Ok(pizzas);
            }

            return Ok(new List<Pizza>());

        }

        [HttpGet("{id}")]
        public IActionResult GetPizzaById(int id)
        {
            var pizza = _service.GetAllPizzas().FirstOrDefault(pizza => pizza.Id == id);

            if (pizza == null) return NotFound();

            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pizza pizza)
        {
            if (!_service.GetAllPizzas().Any(pizz => pizz.Name.Equals(pizza.Name)))
            {
                _service.AddPizza(pizza);
                return CreatedAtAction(nameof(GetPizzaById), new { id = pizza.Id }, pizza);
            }

            return Conflict("Oh no! We already have this pizza :(");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = _service.GetAllPizzas().FirstOrDefault(pizza => pizza.Id == id);

            if (pizza == null) return NotFound();

            _service.DeletePizza(pizza);

            return NoContent();

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pizza updatedPizza)
        {
            var pizzaToUpdate = _service.GetAllPizzas().FirstOrDefault(pizza => pizza.Id == id);

            if (pizzaToUpdate == null) return NotFound();

            _service.UpdatePizza(pizzaToUpdate, updatedPizza);

            return NoContent();
        }
    }
}
