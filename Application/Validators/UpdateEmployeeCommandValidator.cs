using Application.Commands.UpdateEmployee;
using FluentValidation;

namespace Application.Validators;

public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(r => r.Id).NotNull();
        RuleFor(r => r.CompanyId).NotNull();
        RuleFor(r => r.EmployeeUpdate.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.EmployeeUpdate.Age).NotNull();
        RuleFor(r => r.EmployeeUpdate.Position).NotEmpty().MaximumLength(150);
    }
}
