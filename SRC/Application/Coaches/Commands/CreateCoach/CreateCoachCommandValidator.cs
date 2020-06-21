using FluentValidation;

namespace BasketballLeague.Application.Coaches.Commands.CreateCoach
{
    public class CreateCoachCommandValidator : AbstractValidator<CreateCoachCommand>
    {
        public CreateCoachCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .NotEmpty();
            RuleFor(x => x.Surname)
                .MinimumLength(3)
                .NotEmpty();
        }
    }
}
