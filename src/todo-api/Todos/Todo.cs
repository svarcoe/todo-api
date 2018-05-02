using System;

namespace Todo.Api.Todos
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