using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Players.Queries.GetPlayerDetail
{
    public class GetPlayerDetailQuery : IRequest<Player>
    {
        public int Id { get; set; }
    }
}
