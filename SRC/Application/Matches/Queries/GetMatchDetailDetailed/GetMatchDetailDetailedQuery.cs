using MediatR;

namespace BasketballLeague.Application.Matches.Queries.GetMatchDetailDetailed
{
    public class GetMatchDetailDetailedQuery : IRequest<MatchDetailedDto>
    {
        public int Id { get; set; }
    }
}
