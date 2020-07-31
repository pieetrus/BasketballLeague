using BasketballLeague.Application.Common.Validators;
using FluentValidation;

namespace BasketballLeague.Application.User.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
