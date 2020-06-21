using FluentValidation;

namespace BasketballLeague.Application.TeamSeasons.Commands.CreateTeamSeason
{
    public class CreateTeamSeasonCommandValidator : AbstractValidator<CreateTeamSeasonCommand>
    {
        public CreateTeamSeasonCommandValidator()
        {
            RuleFor(x => x.SeasonDivisionId)
                .NotEmpty();
            RuleFor(x => x.TeamId)
                .NotEmpty();
            //RuleFor(x => x.CoachId)
            //    .NotEmpty();
            //RuleFor(x => x.CapitainId)
            //    .NotEmpty();
            RuleFor(x => x.RankingPoints)
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
