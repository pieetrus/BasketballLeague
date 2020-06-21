using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Fouls.Queries.GetFoulDetail
{
    public class GetFoulDetailQuery : IRequest<Foul>
    {
        public int Id { get; set; }
    }
}