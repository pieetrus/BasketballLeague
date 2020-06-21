using MediatR;

namespace BasketballLeague.Application.PlayerSeasons.Commands.DeletePlayerSeason
{
    public class DeletePlayerSeasonCommand : IRequest
    {
        public int Id { get; set; }
    }
}
