using FluentValidation;

namespace BasketballLeague.Application.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name minimum lenght is 3")
                .MaximumLength(30).WithMessage("Name maximum lenght is 30")
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.ShortName)
                .MaximumLength(3).WithMessage("ShortName maximum lenght is 3")
                .NotEmpty().WithMessage("ShortName field is required");
        }
    }
}
