using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Seasons.Queries.GetSeasonDetail
{

    public class GetSeasonDetailQuery : IRequest<Season>
    {
        public int Id { get; set; }
    }
}