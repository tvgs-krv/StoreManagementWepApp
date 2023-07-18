using MediatR;

namespace StoreInfo.Api.Application.Commands.CreateStoreItem;

public class CreateStoreItemCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string CountryCode { get; set; }

    public string Email { get; set; }

    public int Category { get; set; }

    public CreateManagerInfoCommand ManagerInfo { get; set; }
}

public class CreateManagerInfoCommand
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
