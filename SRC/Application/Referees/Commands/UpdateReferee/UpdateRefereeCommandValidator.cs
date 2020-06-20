using FluentValidation;

namespace BasketballLeague.Application.Referees.Commands.UpdateReferee
{
    public class UpdateRefereeCommandValidator : AbstractValidator<UpdateRefereeCommand>
    {
        public UpdateRefereeCommandValidator()
        {
            RuleFor(x => x.JerseyNr)
                .MinimumLength(1)
                .MaximumLength(2);
        }
    }
}
