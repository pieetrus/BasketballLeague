using FluentValidation;

namespace BasketballLeague.Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
    {
        public CreateSeasonCommandValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty();
            RuleFor(x => x.StartDate)
                .NotEmpty();
        }
    }
}
