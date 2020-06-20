using FluentValidation;

namespace BasketballLeague.Application.PlayerMatches.Commands.CreatePlayerMatch
{
    public class CreatePlayerMatchCommandValidator : AbstractValidator<CreatePlayerMatchCommand>
    {


        public CreatePlayerMatchCommandValidator()
        {
            RuleFor(x => x.PlayerId)
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
        }

    }
}
