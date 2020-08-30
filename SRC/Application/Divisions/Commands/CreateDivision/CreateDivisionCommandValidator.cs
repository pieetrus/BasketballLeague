using FluentValidation;

namespace BasketballLeague.Application.Divisions.Commands.CreateDivision
{
    public class CreateDivisionCommandValidator : AbstractValidator<CreateDivisionCommand>
    {
        public CreateDivisionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.ShortName)
                .MaximumLength(6).WithMessage("Short name max length is 6")
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.Level).NotEmpty();
        }
    }
}
