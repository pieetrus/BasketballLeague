using FluentValidation;

namespace BasketballLeague.Application.JumpBalls.Commands.UpdateJumpBall
{
    public class UpdateJumpBallCommandValidator : AbstractValidator<UpdateJumpBallCommand>
    {
        public UpdateJumpBallCommandValidator()
        {
            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4);

            RuleFor(x => (int)x.JumpBallType)
                .InclusiveBetween(1, 5);
        }
    }
}
