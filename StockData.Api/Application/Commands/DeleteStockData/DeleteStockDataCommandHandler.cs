using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Exceptions;
using StockData.Api.Application.Interfaces;

namespace StockData.Api.Application.Commands.DeleteStockData;

public class DeleteStockDataCommandHandler : IRequestHandler<DeleteStockDataCommand, Unit>
{

    private readonly IStockDbContext _dbContext;

    public DeleteStockDataCommandHandler(IStockDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteStockDataCommand request, CancellationToken cancellationToken)
    {
        var stocks = await _dbContext.StockData
            .Where(s => s.StoreItemId == request.StoreItemId)
            .OrderBy(c => c.CreatedOn)
            .ToListAsync(cancellationToken);

        if (stocks == null || !stocks.Any())
            throw new NotFoundException(nameof(StockData), request.StoreItemId);

        _dbContext.StockData.RemoveRange(stocks);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}