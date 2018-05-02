using System;

namespace todo_api.Todos
{
	public class Todo
	{
	    public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
	    public DateTime CreatedAt { get; set; }
	    public DateTime ModifiedAt { get; set; }
	}
}