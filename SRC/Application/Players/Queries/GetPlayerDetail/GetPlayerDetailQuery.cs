using MediatR;

namespace BasketballLeague.Application.Players.Queries.GetPlayerDetail
{
    public class GetPlayerDetailQuery : IRequest<GetPlayerDetailQueryHandler.PlayerDto>
    {
        public int Id { get; set; }
    }
}
