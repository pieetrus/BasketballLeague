using FluentValidation;

namespace BasketballLeague.Application.Coaches.Commands.UpdateCoach
{
    public class UpdateCoachCommandValidator : AbstractValidator<UpdateCoachCommand>
    {
        public UpdateCoachCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3);
            RuleFor(x => x.Surname)
                .MinimumLength(3);
        }
    }
}
