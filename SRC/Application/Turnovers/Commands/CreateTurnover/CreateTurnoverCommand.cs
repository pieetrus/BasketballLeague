﻿using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Turnovers.Commands.CreateTurnover
{
    public class CreateTurnoverCommand : IRequest
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public IncidentType IncidentType { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public int PlayerId { get; set; }
        public TurnoverType TurnoverType { get; set; }



        public class Handler : IRequestHandler<CreateTurnoverCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateTurnoverCommand request, CancellationToken cancellationToken)
            {
                if (request.IncidentType != IncidentType.TURNOVER)
                {
                    throw new Exception("Error creating turnover incident - bad incident type");
                }

                var incident = new Incident
                {
                    MatchId = request.MatchId,
                    Minutes = request.Minutes,
                    Seconds = request.Seconds,
                    IncidentType = request.IncidentType,
                    Quater = request.Quater,
                    Flagged = request.Flagged
                };

                var turnover = new Turnover
                {
                    PlayerId = request.PlayerId,
                    TurnoverType = request.TurnoverType,
                    Incident = incident
                };

                _context.Turnover.Add(turnover);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    return Unit.Value;
                }

                throw new Exception("Error saving changes");
            }
        }
    }
}