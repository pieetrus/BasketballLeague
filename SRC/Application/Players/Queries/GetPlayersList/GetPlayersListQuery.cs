using MediatR;
using System.Collections.Generic;

namespace BasketballLeague.Application.Players.Queries.GetPlayersList
{
    public class GetPlayersListQuery : IRequest<IEnumerable<PlayerListDto>>
    {

        public GetPlayersListQuery(string surnameLetter)
        {
            SurnameLetter = surnameLetter;
        }

        public string SurnameLetter { get; set; }
    }
}
