using MediatR;

namespace BasketballLeague.Application.Seasons.Queries.GetSeasonDetail
{

    public class GetSeasonDetailQuery : IRequest<SeasonDto>
    {
        public int Id { get; set; }
    }
}