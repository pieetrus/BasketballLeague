using FluentValidation;

namespace BasketballLeague.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {

        public CreatePlayerCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name minimum lenght is 3")
                .NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.Surname)
                .MinimumLength(3).WithMessage("Name minimum lenght is 3")
                .NotEmpty().WithMessage("Surname field is required");
            RuleFor(x => (int)x.Position)
                .InclusiveBetween(1, 5).WithMessage("Position must be be beetwen 1-5");
        }

    }
}
