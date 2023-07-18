using MediatR;
using StoreInfo.Api.Application.Interfaces;
using StoreInfo.Api.Application.Models;

namespace StoreInfo.Api.Application.Commands.CreateStoreItem;

public class CreateStoreItemCommandHandler : IRequestHandler<CreateStoreItemCommand, Guid>
{
    private readonly IStoreDbContext _dbContext;

    public CreateStoreItemCommandHandler(IStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateStoreItemCommand request, CancellationToken cancellationToken)
    {
        var storeId = Guid.NewGuid();
        var storeItem = new StoreItem
        {
            Id = storeId,
            Category = request.Category,
            CountryCode = request.CountryCode,
            Email = request.Email,
            Name = request.Name,
            ManagerInfo = new ManagerInfo
            {
                Id = Guid.NewGuid(),
                Email = request.ManagerInfo.Email,
                FirstName = request.ManagerInfo.FirstName,
                LastName = request.ManagerInfo.LastName
            },
            CreatedOn = DateTime.Now.ToUniversalTime(),
            ModifiedOn = DateTime.Now.ToUniversalTime()
        };

        await _dbContext.StoreItems.AddAsync(storeItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return storeItem.Id;
    }
}