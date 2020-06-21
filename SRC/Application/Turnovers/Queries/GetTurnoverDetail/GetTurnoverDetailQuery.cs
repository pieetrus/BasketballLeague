using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Turnovers.Queries.GetTurnoverDetail
{
    public class GetTurnoverDetailQuery : IRequest<Turnover>
    {
        public int Id { get; set; }
    }
}
