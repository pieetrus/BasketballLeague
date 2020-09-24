using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerMatches.Commands.CreatePlayerMatch
{
    public class CreatePlayerMatchCommand : IRequest
    {
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int Fg3a { get; set; }
        public int Fg3m { get; set; }
        public int Fg2a { get; set; }
        public int Fg2m { get; set; }
        public int Fta { get; set; }
        public int Ftm { get; set; }
        public int Orb { get; set; }
        public int Drb { get; set; }
        public int Ast { get; set; }
        public int Stl { get; set; }
        public int Blk { get; set; }
        public int Tov { get; set; }
        public int Fouls { get; set; }
        public int OffFouls { get; set; }


        public class Handler : IRequestHandler<CreatePlayerMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreatePlayerMatchCommand request, CancellationToken cancellationToken)
            {
                var entity = new PlayerMatch
                {
                    PlayerSeasonId = request.PlayerId,
                    MatchId = request.MatchId,
                    Fg3a = request.Fg3a,
                    Fg3m = request.Fg3m,
                    Fg2a = request.Fg2a,
                    Fg2m = request.Fg2m,
                    Fta = request.Fta,
                    Ftm = request.Ftm,
                    Orb = request.Orb,
                    Drb = request.Drb,
                    Ast = request.Ast,
                    Stl = request.Stl,
                    Blk = request.Blk,
                    Tov = request.Tov,
                    Fouls = request.Fouls,
                    OffFouls = request.OffFouls
                };

                _context.PlayerMatch.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }

    }
}