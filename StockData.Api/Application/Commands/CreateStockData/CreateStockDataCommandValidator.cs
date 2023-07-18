using FluentValidation;

namespace StockData.Api.Application.Commands.CreateStockData;

public class CreateStockDataCommandValidator : AbstractValidator<CreateStockDataCommand>
{
    public CreateStockDataCommandValidator()
    {
        RuleFor(command => command.StoreItemId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .WithMessage("Атрибут \"storeItemId\" должен быть заполнен");
    }
}