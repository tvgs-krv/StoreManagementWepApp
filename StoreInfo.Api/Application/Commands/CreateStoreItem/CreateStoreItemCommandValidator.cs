using FluentValidation;

namespace StoreInfo.Api.Application.Commands.CreateStoreItem;

public class CreateStoreItemCommandValidator : AbstractValidator<CreateStoreItemCommand>
{
    public CreateStoreItemCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"name\" должен быть заполнен")
            .MaximumLength(250);
        
        RuleFor(command => command.Category)
            .NotNull()
            .WithMessage("Атрибут \"category\" должен быть заполнен")
            .GreaterThan(0)
            .WithMessage("Атрибут \"category\" должен быть больше 0");

        RuleFor(command => command.CountryCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"countryCode\" должен быть заполнен");

        RuleFor(command => command.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"email\" должен быть заполнен");

        RuleFor(command => command.ManagerInfo)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"managerInfo\" должен быть заполнен");

        RuleFor(command => command.ManagerInfo.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"firstName\" должен быть заполнен");


        RuleFor(command => command.ManagerInfo.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"lastName\" должен быть заполнен");

        RuleFor(command => command.ManagerInfo.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Атрибут \"email\" должен быть заполнен");
    }
}