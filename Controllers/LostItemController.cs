using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication35.Models;

namespace WebApplication35.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LostItemController : ControllerBase
    {
        private static readonly List<LostItem> items = new()
    {
        new() { Id = 1, Name = "Ключи", Place = "Аудитория 205" },
        new() { Id = 2, Name = "Флешка", Place = "Библиотека" },
        new() { Id = 3, Name = "Наушники", Place = "Столовая" },
        new() { Id = 4, Name = "Студенческий билет", Place = "Холл" }
    };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(items);
        }


        [HttpPost]
        public IActionResult Add([FromBody] LostItem item)
        {
            item.Id = items.Count == 0 ? 1 : items.Max(x => x.Id) + 1;
            items.Add(item);
            return StatusCode(201, item);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = items.FirstOrDefault(x => x.Id == id);
            if (x != null)
                return Ok(x);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var x = items.FirstOrDefault(x => x.Id == id);
            if (x != null)
            {
                items.Remove(x);
                return NoContent();
            }
            else
                return NotFound();
        }

        [HttpGet("search")]
        public IActionResult SearchBy([FromQuery] string place)
        {
            var result = items.Where(p => p.Place == place);
            if (result.Any())
                return Ok(result);
            else
                return NoContent();
        }
    }
}
