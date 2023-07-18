using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StoreInfo.Api.Application.Models;

namespace StoreInfo.Api.Infrastructure.EntityTypeConfigurations;

public class StoreConfiguration : IEntityTypeConfiguration<StoreItem>
{
    public void Configure(EntityTypeBuilder<StoreItem> builder)
    {
        builder.HasKey(item => item.Id);
        builder.HasIndex(item => item.Id).IsUnique();
        builder.Property(item => item.Name).HasMaxLength(250);
    }
}