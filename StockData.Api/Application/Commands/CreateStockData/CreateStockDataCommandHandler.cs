using MediatR;
using StockData.Api.Application.Interfaces;

namespace StockData.Api.Application.Commands.CreateStockData;

public class CreateStockDataCommandHandler : IRequestHandler<CreateStockDataCommand, Guid>
{
    private readonly IStockDbContext _dbContext;

    public CreateStockDataCommandHandler(IStockDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateStockDataCommand request, CancellationToken cancellationToken)
    {
        var stockData = new Models.StockData
        {
            Id = Guid.NewGuid(),
            Accuracy = request.Accuracy,
            Backstory = request.Backstory,
            Frontstore = request.Frontstore,
            TotalStock = request.Accuracy + request.Backstory + request.Frontstore,
            OnFloorAvailability = request.OnFloorAvailability,
            ShoppingWindow = request.ShoppingWindow,
            MeanAge = request.MeanAge,
            StoreItemId = request.StoreItemId,
            CreatedOn = DateTime.Now.ToUniversalTime(),
            ModifiedOn = DateTime.Now.ToUniversalTime()
        };

        await _dbContext.StockData.AddAsync(stockData, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return stockData.Id;
    }
}