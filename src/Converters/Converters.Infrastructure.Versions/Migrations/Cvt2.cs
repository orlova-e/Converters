using FluentMigrator;

namespace Converters.Infrastructure.Versions.Migrations;

[Migration(2, "add convertations table")]
public class Cvt2 : Migration
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
            .WithColumn("jsonfileid").AsGuid().NotNullable()
            .WithColumn("xmlfileid").AsGuid().NotNullable();
        
        Create
            .ForeignKey("FK_convertations_jsonfileid")
            .FromTable("convertations").ForeignColumn("jsonfileid")
            .ToTable("files").PrimaryColumn("id");
        
        Create
            .ForeignKey("FK_convertations_xmlfileid")
            .FromTable("convertations").ForeignColumn("xmlfileid")
            .ToTable("files").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("convertations");
    }
}