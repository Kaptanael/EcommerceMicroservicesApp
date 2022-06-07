using Authentication.Application.Contracts.Users;
using FluentValidation;

namespace Authentication.Application.Features.Users.Commands.Registration
{
    public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationCommandValidator()
        {
            RuleFor(u => u.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64);

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64);

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(4, 8);            

        }
    }
}
