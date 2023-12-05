using Application.Commands.CreateEmployee;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Validators;

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(r => r.CompanyId).NotNull();
        RuleFor(r => r.Employee.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.Employee.Age).NotNull();
        RuleFor(r => r.Employee.Position).NotEmpty().MaximumLength(150);
    }

    //public override ValidationResult Validate(ValidationContext<CreateEmployeeCommand> context)
    //{
    //    return context.InstanceToValidate.Employee is null ?
    //        new ValidationResult(new[]
    //        {
    //            new ValidationFailure("EmployeeCreationDto", "EmployeeCreationDto object is null")
    //        })
    //        : base.Validate(context);
    //}
}
