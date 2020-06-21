using FluentValidation;

namespace BasketballLeague.Application.Timeouts.Commands.CreateTimeout
{
    public class CreateTimeoutCommandValidator : AbstractValidator<CreateTimeoutCommand>
    {
        public CreateTimeoutCommandValidator()
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

            RuleFor(x => x.TeamId)
                .NotEmpty();

        }
    }
}