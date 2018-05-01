using FluentMigrator.Builders.Create.Table;

namespace todo_db
{
	internal static class MigrationExtensions
	{
		public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(
			this ICreateTableWithColumnSyntax tableWithColumnSyntax)
		{
			return tableWithColumnSyntax
				.WithColumn("Id")
				.AsInt32()
				.NotNullable()
				.PrimaryKey()
				.Identity();
		}

		public static ICreateTableColumnOptionOrWithColumnSyntax WithTimeStamps(
			this ICreateTableWithColumnSyntax tableWithColumnSyntax)
		{
			return tableWithColumnSyntax
				.WithColumn("CreatedAt").AsDateTime2().NotNullable()
				.WithColumn("ModifiedAt").AsDateTime2().NotNullable();

		}
	}
}