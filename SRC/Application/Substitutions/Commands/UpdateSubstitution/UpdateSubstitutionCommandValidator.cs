﻿using FluentValidation;

namespace BasketballLeague.Application.Substitutions.Commands.UpdateSubstitution
{
    public class UpdateSubstitutionCommandValidator : AbstractValidator<UpdateSubstitutionCommand>
    {
        public UpdateSubstitutionCommandValidator()
        {
            RuleFor(x => x.Minutes)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Seconds)
                .MinimumLength(1)
                .MaximumLength(2);

            RuleFor(x => x.Quater)
                .InclusiveBetween(1, 4);

            RuleFor(x => x.PlayerInId)
                .NotEqual(x => x.PlayerOutId);
        }
    }
}