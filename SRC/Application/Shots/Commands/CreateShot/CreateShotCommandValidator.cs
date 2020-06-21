using FluentValidation;

namespace BasketballLeague.Application.Shots.Commands.CreateShot
{
    public class CreateShotCommandValidator : AbstractValidator<CreateShotCommand>
    {
        public CreateShotCommandValidator()
        {
            RuleFor(x => x.MatchId)
                .NotEmpty();

            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2)
                .NotEmpty();

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2)
                .NotEmpty();

            RuleFor(x => x.Quater)
                .InclusiveBetween(1,4)
                .NotEmpty();

            RuleFor(x => x.PlayerId)
                .NotEmpty();

            RuleFor(x => (int)x.ShotType)
                .InclusiveBetween(1, 7)
                .NotEmpty();


            RuleFor(x => x.IsFastAttack)
                .Must(x => x == false || x == true);

            RuleFor(x => x.IsAccurate)
               .Must(x => x == false || x == true);

            RuleFor(x => x.Value)
                .InclusiveBetween(2,3)
                .NotEmpty();
        }
    }
}
