using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Seasons.Queries.GetSeasonsList
{
    public class GetSeasonListQuery : IRequest<IEnumerable<SeasonDto>>
    {
    }
}