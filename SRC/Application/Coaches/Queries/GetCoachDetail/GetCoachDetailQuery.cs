using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Coaches.Queries.GetCoachDetail
{
    public class GetCoachDetailQuery : IRequest<Coach>
    {
        public int Id { get; set; }
    }
}
