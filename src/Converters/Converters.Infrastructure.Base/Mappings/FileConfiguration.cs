using System;
using Converters.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Converters.Infrastructure.Base.Mappings;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder
            .ToTable("files");

        builder
            .HasKey(x => x.Id)
            .HasName("id");
        
        builder
            .HasIndex(x => x.Id)
            .HasName("PK_files")
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
            .Property(x => x.Data)
            .HasColumnType(DataTypes.Blob)
            .HasColumnName("data")
            .IsRequired();
        
        builder
            .Property<Guid>("convertationid")
            .IsRequired();
    }
}