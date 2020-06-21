using FluentValidation;

namespace BasketballLeague.Application.Turnovers.Commands.UpdateTimeout
{
    public class UpdateTurnoverCommandValidator : AbstractValidator<UpdateTurnoverCommand>
    {
        public UpdateTurnoverCommandValidator()
        {
            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4);
        }
    }
}