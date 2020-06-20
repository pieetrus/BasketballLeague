using FluentValidation;

namespace BasketballLeague.Application.Shots.Commands.UpdateShot
{
    public class UpdateShotCommandValidator : AbstractValidator<UpdateShotCommand>
    {
        public UpdateShotCommandValidator()
        {
            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4);

            RuleFor(x => x.ShotType)
                .InclusiveBetween(1, 7);

            RuleFor(x => x.Value)
                .InclusiveBetween(2, 3);
        }
    }
}
