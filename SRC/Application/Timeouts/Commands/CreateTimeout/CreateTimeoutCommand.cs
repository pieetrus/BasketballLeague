﻿using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Common;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Timeout = BasketballLeague.Domain.Entities.Timeout;

namespace BasketballLeague.Application.Timeouts.Commands.CreateTimeout
{
    public class CreateTimeoutCommand : IRequest
    {
        public int MatchId { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public IncidentType IncidentType { get; set; }
        public int Quater { get; set; }
        public bool Flagged { get; set; }

        public int TeamId { get; set; }



        public class Handler : IRequestHandler<CreateTimeoutCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateTimeoutCommand request, CancellationToken cancellationToken)
            {
                if (request.IncidentType != IncidentType.TIMEOUT)
                {
                    throw new Exception("Error creating timeout incident - bad incident type");
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

                var timeout = new Timeout
                {
                    TeamId = request.TeamId,
                    Incident = incident
                };

                _context.Timeout.Add(timeout);

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