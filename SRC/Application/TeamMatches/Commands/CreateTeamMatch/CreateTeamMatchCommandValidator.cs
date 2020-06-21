using FluentValidation;

namespace BasketballLeague.Application.TeamMatches.Commands.CreateTeamMatch
{
    public class CreateTeamMatchCommandValidator : AbstractValidator<CreateTeamMatchCommand>
    {
        public CreateTeamMatchCommandValidator()
        {
            RuleFor(x => x.TeamId)
                .NotEmpty();
            RuleFor(x => x.MatchId)
                .NotEmpty();

            RuleFor(x => x.Fg3m)
                .LessThanOrEqualTo(x => x.Fg3a);
            RuleFor(x => x.Fg2m)
                .LessThanOrEqualTo(x => x.Fg2a);
            RuleFor(x => x.OffFouls)
                .LessThanOrEqualTo(x => x.Fouls);
            RuleFor(x => x.Ftm)
                .LessThanOrEqualTo(x => x.Fta);
            RuleFor(x => x.BenchPts)
                .LessThanOrEqualTo(x => 2* x.Fg2m + 3 *x.Fg3m);
            RuleFor(x => x.Fastbreakpoints)
               .LessThanOrEqualTo(x => 2 * x.Fg2m + 3 * x.Fg3m);
            RuleFor(x => x.SecondChancePoints)
               .LessThanOrEqualTo(x => 2 * x.Fg2m + 3 * x.Fg3m);
            RuleFor(x => x.PointsFromTurnovers)
               .LessThanOrEqualTo(x => 2 * x.Fg2m + 3 * x.Fg3m);
        }
    }
}
