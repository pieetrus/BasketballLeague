using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.PlayerMatches.Commands.UpdatePlayerMatch
{
    public class UpdatePlayerMatchCommand : IRequest
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? MatchId { get; set; }
        public int? Fg3a { get; set; }
        public int? Fg3m { get; set; }
        public int? Fg2a { get; set; }
        public int? Fg2m { get; set; }
        public int? Fta { get; set; }
        public int? Ftm { get; set; }
        public int? Orb { get; set; }
        public int? Drb { get; set; }
        public int? Ast { get; set; }
        public int? Stl { get; set; }
        public int? Blk { get; set; }
        public int? Tov { get; set; }
        public int? Fouls { get; set; }
        public int? OffFouls { get; set; }

        public class Handler : IRequestHandler<UpdatePlayerMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePlayerMatchCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PlayerMatch.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(PlayerMatch), request.Id);
                }

                entity.PlayerSeasonId = request.PlayerId ?? entity.PlayerSeasonId;
                entity.MatchId = request.MatchId ?? entity.MatchId;
                entity.Fg3a = request.Fg3a ?? entity.Fg3a;
                entity.Fg3m = request.Fg3m ?? entity.Fg3m;
                entity.Fg2a = request.Fg2a ?? entity.Fg2a;
                entity.Fg2m = request.Fg2m ?? entity.Fg2m;
                entity.Fta = request.Fta ?? entity.Fta;
                entity.Ftm = request.Ftm ?? entity.Ftm;
                entity.Orb = request.Orb ?? entity.Orb;
                entity.Ast = request.Ast ?? entity.Ast;
                entity.Stl = request.Stl ?? entity.Stl;
                entity.Blk = request.Blk ?? entity.Blk;
                entity.Tov = request.Tov ?? entity.Tov;
                entity.Fouls = request.Fouls ?? entity.Fouls;
                entity.OffFouls = request.OffFouls ?? entity.OffFouls;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
