using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockData.Api.Application.Models;

namespace StockData.Api.Infrastructure.EntityTypeConfigurations;

public class StockConfiguration : IEntityTypeConfiguration<Application.Models.StockData>
{
    public void Configure(EntityTypeBuilder<Application.Models.StockData> builder)
    {
        builder.HasKey(data => data.Id);
        builder.HasIndex(data => data.Id).IsUnique();
    }
}