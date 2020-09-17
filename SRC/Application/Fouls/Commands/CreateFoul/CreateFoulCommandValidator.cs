using FluentValidation;

namespace BasketballLeague.Application.Fouls.Commands.CreateFoul
{
    public class CreateFoulCommandValidator : AbstractValidator<CreateFoulCommand>
    {
        public CreateFoulCommandValidator()
        {
            // todo validation based on foultype


            RuleFor(x => x.CoachId)
                .NotEmpty()
                .When(x => x.FoulType == Domain.Common.FoulType.COACH_DISQUALIFYING || x.FoulType == Domain.Common.FoulType.COACH_TECHNICAL);

            RuleFor(x => x.PlayerWhoFouledId)
                .NotEmpty()
                .When(x => x.FoulType != Domain.Common.FoulType.COACH_DISQUALIFYING
                && x.FoulType != Domain.Common.FoulType.COACH_TECHNICAL && x.FoulType != Domain.Common.FoulType.BENCH_TECHNICAL);

            RuleFor(x => x.PlayerWhoWasFouledId)
                .NotEmpty()
                .When(x => x.FoulType != Domain.Common.FoulType.COACH_DISQUALIFYING && x.FoulType != Domain.Common.FoulType.COACH_TECHNICAL
                && x.FoulType != Domain.Common.FoulType.TECHNICAL && x.FoulType != Domain.Common.FoulType.DISQUALIFYING
                && x.FoulType != Domain.Common.FoulType.BENCH_TECHNICAL);

            RuleFor(x => (int)x.FoulType)
              .InclusiveBetween(1, 12)
              .NotEmpty();

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
                .InclusiveBetween(1, 4)
                .NotEmpty();
        }
    }
}
