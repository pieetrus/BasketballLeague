using FluentValidation;

namespace BasketballLeague.Application.Fouls.Commands.UpdateFoul
{
    class UpdateFoulCommandValidator : AbstractValidator<UpdateFoulCommand>
    {
        public UpdateFoulCommandValidator()
        {
            //todo validation based on foultype

            RuleFor(x => (int)x.FoulType)
              .InclusiveBetween(1, 9);

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