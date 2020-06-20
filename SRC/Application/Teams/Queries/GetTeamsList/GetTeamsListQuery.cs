using BasketballLeague.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Teams.Queries.GetTeamsList
{
    public class GetTeamsListQuery : IRequest<IEnumerable<Team>>
    {
    }
}
