using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.PlayerMatches.Queries.GetPlayerMatchDetail
{
    public class GetPlayerMatchDetailQuery : IRequest<PlayerMatch>
    {
        public int Id { get; set; }
    }
}
