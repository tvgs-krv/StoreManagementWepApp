using FluentValidation;

namespace StoreInfo.Api.Application.Commands.UpdateStoreItem;

public class UpdateStoreItemCommandValidator : AbstractValidator<UpdateStoreItemCommand>
{
    public UpdateStoreItemCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .NotNull()
            .WithMessage("Необходимо указать Id магазина");
    }
}