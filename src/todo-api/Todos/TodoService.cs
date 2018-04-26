using System.Collections.Generic;
using System.Threading.Tasks;

namespace todo_api.Todos
{
	public interface ITodoService
	{
		Task<IEnumerable<Todo>> GetAll();
	}

	public class TodoService : ITodoService
	{
		public async Task<IEnumerable<Todo>> GetAll()
		{
			return await Task.FromResult(new[] { new Todo() });
		}
	}
}