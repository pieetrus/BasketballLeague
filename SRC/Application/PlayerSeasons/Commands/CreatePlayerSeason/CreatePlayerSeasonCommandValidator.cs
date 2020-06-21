using FluentValidation;

namespace BasketballLeague.Application.PlayerSeasons.Commands.CreatePlayerSeason
{
    public class CreatePlayerSeasonCommandValidator : AbstractValidator<CreatePlayerSeasonCommand>
    {
        public CreatePlayerSeasonCommandValidator()
        {
            RuleFor(x => x.PlayerId)
               .NotEmpty();
            RuleFor(x => x.SeasonDivisionId)
                .NotEmpty();
            RuleFor(x => x.TeamId)
                .NotEmpty();
            RuleFor(x => x.JerseyNr)
                .MinimumLength(1)
                .MaximumLength(2)
                .NotEmpty();

            RuleFor(x => x.Fg3m)
                .LessThanOrEqualTo(x => x.Fg3a);
            RuleFor(x => x.Fg2m)
                .LessThanOrEqualTo(x => x.Fg2a);
            RuleFor(x => x.OffFouls)
                .LessThanOrEqualTo(x => x.Fouls);
            RuleFor(x => x.Ftm)
                .LessThanOrEqualTo(x => x.Fta);
        }
    }
}
