using FluentMigrator;

namespace Converters.Infrastructure.Versions.Migrations;

[Migration(1, "add convertations table")]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create
            .Table("convertations")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("created").AsDateTime().NotNullable()
            .WithColumn("updated").AsDateTime().Nullable()
            .WithColumn("deleted").AsDateTime().Nullable()
            .WithColumn("sessionid").AsGuid().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table("convertations");
    }
}