using System;
using FluentMigrator;

namespace todo_db
{

    [Migration(201805211100)]
	public class AddSeedTodoData : Migration
	{
		public override void Up()
		{
            Insert.IntoTable("Todo").Row(new { Name = "Test Todo 1", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow });
		}

		public override void Down()
		{
			Delete.FromTable("Todo").Row(new { Name = "Test Todo 1" });
		}
	}
}