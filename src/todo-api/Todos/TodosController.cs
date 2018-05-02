using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace todo_api.Todos
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
	    private readonly ITodoService _todoService;

	    public TodosController(ITodoService todoService)
	    {
		    _todoService = todoService;
	    }
        // GET api/todos
        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
	        return await _todoService.GetAll();
        }

        // GET api/todos/5
        [HttpGet("{id}")]
        public async Task<Todo> Get(int id)
        {
            return await _todoService.Get(id);
        }

        // POST api/todos
        [HttpPost]
        public async Task<int> Post([FromBody]Todo value)
        {
            return await _todoService.Insert(value);
        }

        // PUT api/todos/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Todo value)
        {
            await _todoService.Update(id, value);
        }

        // DELETE api/todos/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _todoService.Delete(id);
        }
    }

}
