using FluentMigrator;

namespace todo_db
{
	[Migration(201804302300)]
	public class AddTodoTables : Migration
	{
		public override void Up()
		{
            Create.Table("Todos")
				.WithIdColumn()
				.WithTimeStamps()
				.WithColumn("Name").AsString().NotNullable()
				.WithColumn("IsDone").AsBoolean().WithDefaultValue(false);
		}

		public override void Down()
		{
			Delete.Table("Todos");
		}
	}
}