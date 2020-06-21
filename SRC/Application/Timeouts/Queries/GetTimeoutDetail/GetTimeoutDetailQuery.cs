using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Timeouts.Queries.GetTimeoutDetail
{
    public class GetTimeoutDetailQuery : IRequest<Timeout>
    {
        public int Id { get; set; }
    }
}
