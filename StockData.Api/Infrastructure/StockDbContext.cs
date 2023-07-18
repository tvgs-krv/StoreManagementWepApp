using Microsoft.EntityFrameworkCore;
using StockData.Api.Application.Interfaces;
using StockData.Api.Infrastructure.EntityTypeConfigurations;

namespace StockData.Api.Infrastructure;

public class StockDbContext : DbContext, IStockDbContext
{
    public DbSet<Application.Models.StockData> StockData { get; set; }

    public StockDbContext(DbContextOptions<StockDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new StockConfiguration());
        base.OnModelCreating(builder);
    }

}