using contactsApi.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace contactsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<Todoitem> todoList = new()
        {
            new Todoitem { id = 1, title = "learn api", iscompleted = false },
            new Todoitem { id = 2, title = "make a video", iscompleted = false }
        };

        // GET: api/Todo
        [HttpGet]
        public ActionResult<List<Todoitem>> GetAll()
        {
            return Ok(todoList);
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<Todoitem> GetById(int id) // 1. Changed return type to a single Todoitem
        {
            var item = todoList.FirstOrDefault(x => x.id == id);

            // 2. Check if the item actually exists
            if (item == null)
            {
                return NotFound(); // Returns a 404 status code
            }

            // 3. Return the specific item, not the whole list
            return Ok(item);
        }
        [HttpPost]
        public ActionResult<Todoitem> Create(Todoitem newItem)
        {
            int newId = todoList.Any() ? todoList.Max(t => t.id) + 1 : 1;
            newItem.id = newId;

            todoList.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.id }, newItem);
        }
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, Todoitem updatedItem)
        {
            var existingItem = todoList.FirstOrDefault(x => x.id == id);

            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.title = updatedItem.title;
            existingItem.iscompleted = updatedItem.iscompleted;
            return NoContent();
        }
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = todoList.FirstOrDefault(x => x.id == id);
            if (item == null)
            {
                return NotFound();
            }
            todoList.Remove(item);
            return NoContent();
        }
    }
    
}