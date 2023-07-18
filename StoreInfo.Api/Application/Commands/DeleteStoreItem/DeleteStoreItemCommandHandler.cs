using MediatR;
using Services.Common.Exceptions;
using StoreInfo.Api.Application.Interfaces;
using StoreInfo.Api.Application.Models;

namespace StoreInfo.Api.Application.Commands.DeleteStoreItem;

public class DeleteStoreItemCommandHandler : IRequestHandler<DeleteStoreItemCommand, Unit>
{
    private readonly IStoreDbContext _dbContext;

    public DeleteStoreItemCommandHandler(IStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteStoreItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.StoreItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(StoreItem), request.Id);

        _dbContext.StoreItems.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}