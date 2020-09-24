using FluentValidation;

namespace BasketballLeague.Application.Matches.Commands.EndMatch
{
    class EndMatchCommandValidator : AbstractValidator<EndMatchCommand>
    {
        public EndMatchCommandValidator()
        {

        }
    }
}