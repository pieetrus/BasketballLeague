using FluentValidation;

namespace BasketballLeague.Application.FreeThrows.Commands.UpdateFreeThrow
{
    public class UpdateFreeThrowCommandValidator : AbstractValidator<UpdateFreeThrowCommand>
    {
        public UpdateFreeThrowCommandValidator()
        {
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
              .InclusiveBetween(1, 12);

            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4);

            RuleFor(x => x.AccurateShots)
                .InclusiveBetween(0, 3);

            RuleFor(x => x.Attempts)
                .InclusiveBetween(1, 3);
        }
    }
}
