using StockData.Api.Application.Models;

namespace StockData.Api.Application.Queries;

public interface IStockQueries
{
    public Task<IEnumerable<Models.StockData>> GetStocksDataByStoreIdAsync(Guid storeId, CancellationToken cancellationToken);
    public Task<StockDataSum> GetSumOfStockDataAsync(Guid storeId, CancellationToken cancellationToken);

}