using Converters.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Converters.Infrastructure.Base.Mappings;

public class ConvertationConfiguration : IEntityTypeConfiguration<Convertation>
{
    public void Configure(EntityTypeBuilder<Convertation> builder)
    {
        builder
            .ToTable("convertations");

        builder
            .HasKey(x => x.Id)
            .HasName("id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_convertations")
            .IsUnique();
        
        builder
            .Property(x => x.Id)
            .HasColumnType(DataTypes.Uuid)
            .HasColumnName("id")
            .IsRequired();
        
        builder
            .Property(x => x.IsDeleted)
            .HasColumnType(DataTypes.Boolean)
            .HasColumnName("isdeleted")
            .IsRequired();
        
        builder
            .Property(x => x.Name)
            .HasColumnType(DataTypes.Text)
            .HasColumnName("name")
            .IsRequired();
        
        builder
            .Property(x => x.Created)
            .HasColumnType(DataTypes.TimestampWithoutTimeZone)
            .HasColumnName("created")
            .IsRequired();
        
        builder
            .Property(x => x.Updated)
            .HasColumnType(DataTypes.TimestampWithoutTimeZone)
            .HasColumnName("updated")
            .IsRequired(false);
        
        builder
            .Property(x => x.Deleted)
            .HasColumnType(DataTypes.TimestampWithoutTimeZone)
            .HasColumnName("deleted")
            .IsRequired(false);
    }
}