using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Matches.Queries.GetMatchDetail
{

    public class GetMatchDetailQuery : IRequest<Match>
    {
        public int Id { get; set; }
    }
}
