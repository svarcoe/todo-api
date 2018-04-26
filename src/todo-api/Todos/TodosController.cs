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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/todos
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/todos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/todos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
