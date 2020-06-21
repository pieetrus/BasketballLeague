using FluentValidation;

namespace BasketballLeague.Application.PlayerSeasons.Commands.UpdatePlayerSeason
{
    public class UpdatePlayerSeasonCommandValidator : AbstractValidator<UpdatePlayerSeasonCommand>
    {
        public UpdatePlayerSeasonCommandValidator()
        {
            RuleFor(x => x.Fg3m)
                .LessThanOrEqualTo(x => x.Fg3a);
            RuleFor(x => x.Fg2m)
                .LessThanOrEqualTo(x => x.Fg2a);
            RuleFor(x => x.OffFouls)
                .LessThanOrEqualTo(x => x.Fouls);
            RuleFor(x => x.Ftm)
                .LessThanOrEqualTo(x => x.Fta);
            RuleFor(x => x.JerseyNr)
                .MinimumLength(1)
                .MaximumLength(2);
        }
    }
}
