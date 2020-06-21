using FluentValidation;

namespace BasketballLeague.Application.JumpBalls.Commands.CreateJumpBall
{
    public class CreateJumpBallCommandValidator : AbstractValidator<CreateJumpBallCommand>
    {
        public CreateJumpBallCommandValidator()
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

            RuleFor(x => x.IncidentType)
                .NotEmpty();

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4)
                .NotEmpty();

            RuleFor(x => (int)x.JumpBallType)
                .InclusiveBetween(1, 5)
                .NotEmpty();
        }
    }
}
