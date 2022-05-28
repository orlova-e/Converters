using FluentMigrator;

namespace Converters.Infrastructure.Versions.Migrations;

[Migration(1, "add files table")]
public class Cvt1 : Migration
{
    public override void Up()
    {
        Create
            .Table("files")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("data").AsBinary().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table("files");
    }
}