using FluentValidation;

namespace BasketballLeague.Application.Divisions.Commands.UpdateDivision
{
    public class UpdateDivisionCommandValidator : AbstractValidator<UpdateDivisionCommand>
    {
        public UpdateDivisionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.ShortName)
                .MaximumLength(6)
                .NotEmpty();
        }
    }
}
