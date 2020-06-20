using FluentValidation;

namespace BasketballLeague.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        public CreateMatchCommandValidator()
        {
            RuleFor(x => x.SeasonDivisionId)
                .NotEmpty();
            RuleFor(x => x.TeamGuestId)
                .NotEmpty();
            RuleFor(x => x.TeamHomeId)
                .NotEmpty();
            RuleFor(x => x.StartDate)
                .NotEmpty();
        }
    }
}
