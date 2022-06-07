using Authentication.Application.Contracts.Users;
using FluentValidation;

namespace Authentication.Application.Features.Users.Commands.Login
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginCommandValidator()
        {            

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(64);

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Length(4, 8);

        }
    }
}
