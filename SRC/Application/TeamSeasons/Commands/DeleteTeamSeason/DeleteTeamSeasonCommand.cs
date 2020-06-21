using MediatR;

namespace BasketballLeague.Application.TeamSeasons.Commands.DeleteTeamSeason
{
    public class DeleteTeamSeasonCommand : IRequest
    {
        public int Id { get; set; }
    }
}
