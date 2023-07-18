using Microsoft.EntityFrameworkCore;

namespace StockData.Api.Application.Interfaces;

public interface IStockDbContext
{
    DbSet<Models.StockData> StockData { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}