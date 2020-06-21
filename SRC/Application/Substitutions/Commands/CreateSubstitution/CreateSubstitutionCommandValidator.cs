using FluentValidation;

namespace BasketballLeague.Application.Substitutions.Commands.CreateSubstitution
{
    public class CreateSubstitutionCommandValidator : AbstractValidator<CreateSubstitutionCommand>
    {
        public CreateSubstitutionCommandValidator()
        {
            RuleFor(x => x.MatchId)
                .NotEmpty();

            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2)
                .NotEmpty();

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2)
                .NotEmpty();

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4)
                .NotEmpty();

            RuleFor(x => x.PlayerOutId)
                .NotEmpty();

            RuleFor(x => x.PlayerInId)
                .NotEqual(x => x.PlayerOutId)
                .NotEmpty();
        }
    }
}