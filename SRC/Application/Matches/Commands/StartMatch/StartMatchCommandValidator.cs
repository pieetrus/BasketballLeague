using FluentValidation;

namespace BasketballLeague.Application.Matches.Commands.StartMatch
{
    public class StartMatchCommandValidator : AbstractValidator<StartMatchCommand>
    {
        public StartMatchCommandValidator()
        {
        }
    }
}
