using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Exceptions;
using StoreInfo.Api.Application.Interfaces;
using StoreInfo.Api.Application.Models;

namespace StoreInfo.Api.Application.Commands.UpdateStoreItem;

public class UpdateStoreItemCommandHandler : IRequestHandler<UpdateStoreItemCommand, Unit>
{
    private readonly IStoreDbContext _dbContext;

    public UpdateStoreItemCommandHandler(IStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateStoreItemCommand command, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.StoreItems
            .Include(b => b.ManagerInfo)
            .FirstOrDefaultAsync(s => s.Id == command.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(StoreItem), command.Id);

        UpdateFieldIfChanged<StoreItem>(entity, nameof(entity.Name), command.Name);
        UpdateFieldIfChanged<StoreItem>(entity, nameof(entity.Category), command.Category);
        UpdateFieldIfChanged<StoreItem>(entity, nameof(entity.CountryCode), command.CountryCode);
        UpdateFieldIfChanged<StoreItem>(entity, nameof(entity.Email), command.Email);
        UpdateFieldIfChanged<ManagerInfo>(entity.ManagerInfo, nameof(entity.ManagerInfo.Email), command.ManagerInfo?.Email);
        UpdateFieldIfChanged<ManagerInfo>(entity.ManagerInfo, nameof(entity.ManagerInfo.FirstName), command.ManagerInfo?.FirstName);
        UpdateFieldIfChanged<ManagerInfo>(entity.ManagerInfo, nameof(entity.ManagerInfo.LastName), command.ManagerInfo?.LastName);
        entity.ModifiedOn = DateTime.Now.ToUniversalTime();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private void UpdateFieldIfChanged<T>(T entity, string fieldName, object? newValue)
    {
        Type entityType = typeof(T);
        var property = entityType.GetProperty(fieldName);

        if (property == null)
            return;

        var currentValue = property.GetValue(entity);
        if (currentValue == null && newValue != null)
        {
            property.SetValue(entity, newValue);
            return;
        }

        if (currentValue != null && newValue != null &&
            !currentValue.GetHashCode().Equals(newValue.GetHashCode()))
        {
            property.SetValue(entity, newValue);
            return;
        }
    }
}