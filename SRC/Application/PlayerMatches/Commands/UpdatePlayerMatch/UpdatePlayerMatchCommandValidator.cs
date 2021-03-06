﻿using FluentValidation;

namespace BasketballLeague.Application.PlayerMatches.Commands.UpdatePlayerMatch
{
    public class UpdatePlayerMatchCommandValidator : AbstractValidator<UpdatePlayerMatchCommand>
    {
        public UpdatePlayerMatchCommandValidator()
        {
            RuleFor(x => x.Fg3m)
                .LessThanOrEqualTo(x => x.Fg3a);
            RuleFor(x => x.Fg2m)
                .LessThanOrEqualTo(x => x.Fg2a);
            RuleFor(x => x.OffFouls)
                .LessThanOrEqualTo(x => x.Fouls);
            RuleFor(x => x.Ftm)
                .LessThanOrEqualTo(x => x.Fta);
        }
    }
}
