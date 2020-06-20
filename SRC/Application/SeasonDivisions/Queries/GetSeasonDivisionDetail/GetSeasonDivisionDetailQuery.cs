using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.SeasonDivisions.Queries.GetSeasonDivisionDetail
{

    public class GetSeasonDivisionDetailQuery : IRequest<SeasonDivision>
    {
        public int Id { get; set; }
    }
}