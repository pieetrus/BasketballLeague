using FluentValidation;

namespace BasketballLeague.Application.Timeouts.Commands.UpdateTimeout
{
    class UpdateTimeoutCommandValidator : AbstractValidator<UpdateTimeoutCommand>
    {
        public UpdateTimeoutCommandValidator()
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