using FluentValidation;

namespace BasketballLeague.Application.Turnovers.Commands.CreateTurnover
{
    class CreateTurnoverCommandValidator : AbstractValidator<CreateTurnoverCommand>
    {
        public CreateTurnoverCommandValidator()
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

            RuleFor(x => x.IncidentType)
                .NotEmpty();

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4)
                .NotEmpty();

            RuleFor(x => x.PlayerId)
                .NotEmpty();
            RuleFor(x => x.TurnoverType)
                .NotEmpty();
        }
    }
}