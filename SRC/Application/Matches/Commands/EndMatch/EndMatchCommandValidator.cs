using FluentValidation;

namespace BasketballLeague.Application.Matches.Commands.EndMatch
{
    public class EndMatchCommandValidator : AbstractValidator<EndMatchCommand>
    {
        public EndMatchCommandValidator()
        {

        }
    }
}