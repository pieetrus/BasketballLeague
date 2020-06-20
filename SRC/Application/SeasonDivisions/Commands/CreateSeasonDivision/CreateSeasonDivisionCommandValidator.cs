using FluentValidation;

namespace BasketballLeague.Application.SeasonDivisions.Commands.CreateSeasonDivision
{
    public class CreateSeasonDivisionCommandValidator : AbstractValidator<CreateSeasonDivisionCommand>
    {
        public CreateSeasonDivisionCommandValidator()
        {

            RuleFor(x => x.SeasonId)
                .NotEmpty();

            RuleFor(x => x.DivisionId)
                .NotEmpty();
        }
    }
}
