using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.TeamSeasons.Queries.GetTeamSeasonsList
{
    public class GetTeamSeasonListQuery : IRequest<IEnumerable<TeamSeason>>
    {
    }
}
