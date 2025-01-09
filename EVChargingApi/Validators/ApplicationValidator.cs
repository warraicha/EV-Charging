using EVChargingAPI.Models;
using FluentValidation;

namespace EVChargingAPI.Validators;
public class ApplicationValidator : AbstractValidator<Application>
{
    public ApplicationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.VehicleRegistrationNumber)
            .NotEmpty().WithMessage("Vehicle registration number is required.")
            .Matches("^[A-Za-z0-9 ]+$").WithMessage("Invalid vehicle registration format.");

        RuleFor(x => x.Address.Line1)
            .NotEmpty().WithMessage("Address Line 1 is required.")
            .MaximumLength(50).WithMessage("Address Line 1 must be less than 50 characters.");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Address.Postcode)
            .NotEmpty().WithMessage("Postcode is required.")
            .Matches("^[A-Za-z0-9 ]+$").WithMessage("Invalid postcode format.");
    }
}
