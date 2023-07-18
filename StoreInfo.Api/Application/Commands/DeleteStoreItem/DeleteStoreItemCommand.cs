using MediatR;

namespace StoreInfo.Api.Application.Commands.DeleteStoreItem;

public class DeleteStoreItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}