using BasketballLeague.Application.Common.Interfaces;
using BasketballLeague.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasketballLeague.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }


        public class Handler : IRequestHandler<CreateTeamCommand>
        {
            private readonly IBasketballLeagueDbContext _context;

            public Handler(IBasketballLeagueDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
            {
                var entity = new Team
                {
                    Name = request.Name,
                    ShortName = request.ShortName,
                    LogoUrl = request.LogoUrl
                };


                _context.Team.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
