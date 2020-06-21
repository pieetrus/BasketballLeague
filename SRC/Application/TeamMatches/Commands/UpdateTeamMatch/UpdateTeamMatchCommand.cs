using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.TeamMatches.Commands.UpdateTeamMatch
{
    public class UpdateTeamMatchCommand : IRequest
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
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
        public int? BenchPts { get; set; }
        public int? Fastbreakpoints { get; set; }
        public int? SecondChancePoints { get; set; }
        public int? PointsFromTurnovers { get; set; }
        public int? OffFouls { get; set; }

        public class Handler : IRequestHandler<UpdateTeamMatchCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateTeamMatchCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TeamMatch.FindAsync(request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TeamMatch), request.Id);
                }

                entity.TeamId = request.TeamId ?? entity.TeamId;
                entity.MatchId = request.MatchId ?? entity.MatchId;
                entity.Fg3a = request.Fg3a ?? entity.Fg3a;
                entity.Fg3m = request.Fg3m ?? entity.Fg3m;
                entity.Fg2a = request.Fg2a ?? entity.Fg2a;
                entity.Fg2m = request.Fg2m ?? entity.Fg2m;
                entity.Fta = request.Fta ?? entity.Fta;
                entity.Ftm = request.Ftm ?? entity.Ftm;
                entity.Orb = request.Orb ?? entity.Orb;
                entity.Drb = request.Drb ?? entity.Drb;
                entity.Ast = request.Ast ?? entity.Ast;
                entity.Stl = request.Stl ?? entity.Stl;
                entity.Blk = request.Blk ?? entity.Blk;
                entity.Tov = request.Tov ?? entity.Tov;
                entity.Fouls = request.Fouls ?? entity.Fouls;
                entity.BenchPts = request.BenchPts ?? entity.BenchPts;
                entity.Fastbreakpoints = request.Fastbreakpoints ?? entity.Fastbreakpoints;
                entity.SecondChancePoints = request.SecondChancePoints ?? entity.SecondChancePoints;
                entity.PointsFromTurnovers = request.PointsFromTurnovers ?? entity.PointsFromTurnovers;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
