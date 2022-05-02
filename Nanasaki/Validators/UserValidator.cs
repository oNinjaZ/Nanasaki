using FluentValidation;
using Nanasaki.Models;

namespace Nanasaki.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Username)
        .Length(3, 16)
        .WithMessage("Must be between 3-16 characters long.");
    }
}