using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.PlayerSeasons.Queries.GetPlayerSeasonDetail
{
    public class GetPlayerSeasonDetailQuery : IRequest<PlayerSeason>
    {
        public int Id { get; set; }
    }
}