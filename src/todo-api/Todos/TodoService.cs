using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Todo.Api.Todos
{
	public interface ITodoService
	{
	    Task<IEnumerable<Todo>> GetAll();
	    Task<int> Insert(Todo todo);
	    Task Update(int id, Todo todo);
	    Task Delete(Todo todo);
	    Task Delete(int todoId);
	    Task<Todo> Get(int id);
	}

	public class TodoService : ITodoService
	{
	    private readonly ISqlConnectionFactory _sqlConnectionFactory;

	    public TodoService(ISqlConnectionFactory sqlConnectionFactory)
	    {
	        _sqlConnectionFactory = sqlConnectionFactory;
	    }

		public async Task<IEnumerable<Todo>> GetAll()
		{
		    using (SqlConnection connection = _sqlConnectionFactory.GetConnection())
		    {
		        return await connection.GetListAsync<Todo>();
		    }
	    }
        
	    public async Task<Todo> Get(int id)
	    {
	        using (SqlConnection connection = _sqlConnectionFactory.GetConnection())
	        {
	            return await connection.GetAsync<Todo>(id);
	        }
        }

        public async Task<int> Insert(Todo todo)
	    {
	        using (SqlConnection connection = _sqlConnectionFactory.GetConnection())
	        {
	            todo.CreatedAt = todo.ModifiedAt = DateTime.UtcNow;
	            return await connection.InsertAsync<int>(todo);
	        }
	    }

        public async Task Update(int id, Todo todo)
	    {
	        using (SqlConnection connection = _sqlConnectionFactory.GetConnection())
	        {
	            todo.Id = id;
	            await connection.UpdateAsync(todo);
	        }
	    }

	    public async Task Delete(Todo todo)
	    {
	        using (SqlConnection connection = _sqlConnectionFactory.GetConnection())
	        {
	            await connection.DeleteAsync(todo);
	        }
	    }
        
	    public async Task Delete(int id)
	    {
	        using (SqlConnection connection = _sqlConnectionFactory.GetConnection())
	        {
	            await connection.DeleteAsync(id);
	        }
	    }
    }
}