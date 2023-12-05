using Application.Commands.UpdateCompany;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Validators;

public sealed class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator() 
    {
        RuleFor(c => c.Id).NotNull().NotEmpty();
        RuleFor(c => c.CompanyUpdate.Name).NotEmpty().MaximumLength(60);
        RuleFor(c => c.CompanyUpdate.Address).NotEmpty().MaximumLength(500);
    }

    public override ValidationResult Validate(ValidationContext<UpdateCompanyCommand> context)
    {
        return context.InstanceToValidate.CompanyUpdate is null ?
            new ValidationResult(new[]
            {
                new ValidationFailure("CompanyUpdateDto", "CompanyUpdateDto object is null")
            }) : base.Validate(context);
    }
}
