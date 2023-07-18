using MediatR;
using System.Runtime.Serialization;

namespace StoreInfo.Api.Application.Commands.UpdateStoreItem;

public class UpdateStoreItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? CountryCode { get; set; }

    public string? Email { get; set; }

    public int? Category { get; set; }

    public UpdateManagerInfoCommand? ManagerInfo { get; set; }
}

public class UpdateManagerInfoCommand
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
