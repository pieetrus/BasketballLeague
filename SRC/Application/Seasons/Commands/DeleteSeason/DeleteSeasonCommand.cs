using MediatR;

namespace BasketballLeague.Application.Seasons.Commands.DeleteSeason
{
    public class DeleteSeasonCommand : IRequest
    {
        public int Id { get; set; }
    }
}
