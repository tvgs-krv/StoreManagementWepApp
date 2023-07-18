using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Exceptions;
using StockData.Api.Application.Interfaces;
using StockData.Api.Application.Models;

namespace StockData.Api.Application.Queries;

public class StockQueries : IStockQueries
{
    private readonly IStockDbContext _dbContext;

    public StockQueries(IStockDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.StockData>> GetStocksDataByStoreIdAsync(Guid storeId, CancellationToken cancellationToken)
    {
        return await _dbContext.StockData
            .Where(s => s.StoreItemId == storeId)
            .OrderBy(c => c.CreatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<StockDataSum> GetSumOfStockDataAsync(Guid storeId, CancellationToken cancellationToken)
    {
        var data = await _dbContext.StockData
            .Where(s => s.StoreItemId == storeId)
            .ToListAsync(cancellationToken);
       
        if (data == null || !data.Any())
            throw new NotFoundException(nameof(StockData), storeId);
       
        var result = new StockDataSum
        {
            MinTotalStock = data.Min(x => x.TotalStock),
            MaxTotalStock = data.Max(x => x.TotalStock),
            MedTotalStock = data.Average(x => x.TotalStock),

            MinAccuracy = data.Min(x => x.Accuracy),
            MaxAccuracy = data.Max(x => x.Accuracy),
            MedAccuracy = data.Average(x => x.Accuracy),

            MinOnFloorAvailability = data.Min(x => x.OnFloorAvailability),
            MaxOnFloorAvailability = data.Max(x => x.OnFloorAvailability),
            MedOnFloorAvailability = data.Average(x => x.OnFloorAvailability),

            MinMeanAge = data.Min(x => x.MeanAge),
            MaxMeanAge = data.Max(x => x.MeanAge),
            MedMeanAge = data.Average(x => x.MeanAge)
        };

        return result;
    }

}