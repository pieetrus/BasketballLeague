using FluentValidation;

namespace BasketballLeague.Application.Fouls.Commands.CreateFoul
{
    public class CreateFoulCommandValidator : AbstractValidator<CreateFoulCommand>
    {
        public CreateFoulCommandValidator()
        {
            // todo validation based on foultype

            RuleFor(x => x.PlayerWhoFouledId).NotEmpty().When(x => x.CoachId == null || x.PlayerWhoWasFouledId != null);
            RuleFor(x => x.CoachId).NotEmpty().When(x => x.PlayerWhoFouledId == null);

            RuleFor(x => (int)x.FoulType)
              .InclusiveBetween(1, 9)
              .NotEmpty();

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
        }
    }
}
