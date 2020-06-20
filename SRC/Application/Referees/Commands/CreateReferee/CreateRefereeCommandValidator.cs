using FluentValidation;

namespace BasketballLeague.Application.Referees.Commands.CreateReferee
{
    public class CreateRefereeCommandValidator : AbstractValidator<CreateRefereeCommand>
    {
        public CreateRefereeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Surname)
                .NotEmpty();

            RuleFor(x => x.JerseyNr)
                .MinimumLength(1)
                .MaximumLength(2);
        }
    }
}
