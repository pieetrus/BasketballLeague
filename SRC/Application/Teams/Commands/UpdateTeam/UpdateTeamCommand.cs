using BasketballLeague.Application.Common.Exceptions;
using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }


        public class Handler : IRequestHandler<UpdateTeamCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Team.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Team), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.ShortName = request.ShortName ?? entity.ShortName;
                entity.LogoUrl = request.LogoUrl ?? entity.LogoUrl;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
